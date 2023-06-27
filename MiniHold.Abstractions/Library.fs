namespace MiniHold.Abstractions

open System
open FSharp.Control
open I8Beef.Ecobee
open I8Beef.Ecobee.Protocol.Objects
open I8Beef.Ecobee.Protocol.Thermostat
open I8Beef.Ecobee.Protocol.Functions
open I8Beef.Ecobee.Protocol

type TemperatureOffset = OffsetFarenheit of decimal | OffsetCelsius of double

[<StructuredFormatDisplay("{TemperatureString}")>]
type Temperature = Temperature of int
with
    member this.Tenths = let (Temperature x) = this in x
    member this.Farenheit = (decimal this.Tenths / 10m)
    member this.FarenheitString = sprintf "%d°F" (this.Farenheit |> round |> int)
    member this.Celsius = (float this.Farenheit - 32.0) / 1.8
    member this.CelsiusString = sprintf "%d°C" (this.Celsius |> round |> int)
    member this.TemperatureString = String.concat " / " [this.FarenheitString; this.CelsiusString]

    static member FromFarenheit degrees = degrees * 10m |> Math.Round |> int |> Temperature
    static member FromCelsius degrees = degrees * 1.8 + 32.0 |> decimal |> Temperature.FromFarenheit

    member this.AddFarenheit degrees = Temperature.FromFarenheit (this.Farenheit + degrees)
    member this.AddCelsius degrees = Temperature.FromCelsius (this.Celsius + degrees)

[<StructuredFormatDisplay("{PercentageString}")>]
type Humidity = Humidity of int
with
    member this.PercentageString = let (Humidity x) = this in sprintf "%d%%" x

type TempRange = {
    HeatTemp: Temperature
    CoolTemp: Temperature
    Fan: string
} with
    member this.WithHeatTemp x = { this with HeatTemp = x }
    member this.WithCoolTemp x = { this with CoolTemp = x }
    member this.WithFan x = { this with Fan = x }

type Readings = {
    Temperature: Temperature list
    Humidity: Humidity list
}

type Sensor = {
    Name: string
    Occupied: bool
    Readings: Readings
}

type Weather = {
    Condition: string
    Temperature: Temperature
    Humidity: Humidity
}

type Program = {
    Name: string
    CoolTemp: Temperature
    HeatTemp: Temperature
}

type Alert = {
    DateTime: DateTime
    Text: string
}

type Override = {
    At: TempRange
    StartDate: DateTime
    EndDate: DateTime
    Running: bool
}

type Event = {
    EventType: string
    AbsoluteTemperatureRanges: TempRange list
    StartDate: Nullable<DateTime>
    EndDate: Nullable<DateTime>
    Running: bool
}

type ThermostatInformation = {
    EquipmentStatus: string list
    Desired: TempRange
    Actual: Readings
    Sensors: Sensor list
    Weather: Weather
    Program: Program
    Alerts: Alert list
    Events: Event list
}

type ThermostatClient(client: IClient, thermostat: Thermostat) =
    let timeZone = TimeZoneInfo.FindSystemTimeZoneById(thermostat.Location.TimeZone)

    member _.Name = thermostat.Name
    member _.Brand = thermostat.Brand
    member _.Version = thermostat.Version.ThermostatFirmwareVersion
    member _.TimeZone = timeZone

    member _.ToThermostatTime(time: DateTime) = TimeZoneInfo.ConvertTime(time, timeZone)

    static member GetAllAsync(client: IClient) = taskSeq {
        let mutable page = 1
        let mutable finished = false
        while not finished do
            let request = new ThermostatRequest()
            request.Page <- new Page(CurrentPage = page)
            request.Selection <- new Selection(SelectionType = "registered", SelectionMatch = "")
            request.Selection.IncludeLocation <- true
            request.Selection.IncludeVersion <- true
            let! response = client.GetAsync<ThermostatRequest, ThermostatResponse>(request)
            for t in response.ThermostatList do
                new ThermostatClient(client, t)
            if request.Page.TotalPages.HasValue && request.Page.TotalPages.Value <= page then
                page <- request.Page.TotalPages.Value
            else
                finished <- true
    }

    static member GetAllAsListAsync(client: IClient) =
        ThermostatClient.GetAllAsync(client) |> TaskSeq.toListAsync

    member _.GetInformationAsync() = task {
        let request = new ThermostatRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Selection.IncludeAlerts <- true
        request.Selection.IncludeEquipmentStatus <- true
        request.Selection.IncludeEvents <- true
        request.Selection.IncludeProgram <- true
        request.Selection.IncludeRuntime <- true
        request.Selection.IncludeSensors <- true
        request.Selection.IncludeWeather <- true
        let! response = client.GetAsync<ThermostatRequest, ThermostatResponse>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
        let t = Seq.exactlyOne response.ThermostatList
        let currentWeather = Seq.head t.Weather.Forecasts
        return {
            EquipmentStatus = t.EquipmentStatus.Split(',') |> Seq.except [""] |> Seq.toList
            Desired = {
                HeatTemp = Temperature t.Runtime.DesiredHeat.Value
                CoolTemp = Temperature t.Runtime.DesiredCool.Value
                Fan = t.Runtime.DesiredFanMode
            }
            Actual = {
                Temperature = [
                    if t.Runtime.ActualTemperature.HasValue then
                        Temperature t.Runtime.ActualTemperature.Value
                ]
                Humidity = [
                    if t.Runtime.ActualHumidity.HasValue then
                        Humidity t.Runtime.ActualHumidity.Value
                ]
            }
            Sensors = [
                for s in t.RemoteSensors do {
                    Name = s.Name
                    Occupied =
                        s.Capability
                        |> Seq.where (fun c -> c.Type = "occupancy")
                        |> Seq.exists (fun c -> Boolean.Parse c.Value = true)
                    Readings = {
                        Temperature = [
                            for c in s.Capability do
                                if c.Type = "temperature" then
                                    Temperature (Int32.Parse c.Value)
                        ]
                        Humidity = [
                            for c in s.Capability do
                                if c.Type = "humidity" then
                                    Humidity (Int32.Parse c.Value)
                        ]
                    }
                }
            ]
            Weather = {
                Condition = currentWeather.Condition
                Temperature = Temperature currentWeather.Temperature.Value
                Humidity = Humidity currentWeather.RelativeHumidity.Value
            }
            Program = Seq.head (seq {
                for c in t.Program.Climates do
                    if c.ClimateRef = t.Program.CurrentClimateRef then
                        {
                            Name = c.Name
                            CoolTemp = Temperature c.CoolTemp.Value
                            HeatTemp = Temperature c.HeatTemp.Value
                        }
            })
            Alerts = [
                for a in t.Alerts do {
                    DateTime = DateTime.Parse($"{a.Date} {a.Time}")
                    Text = a.Text
                }
            ]
            Events = [
                for e in t.Events do
                    {
                        EventType = e.Type
                        AbsoluteTemperatureRanges = [
                            if e.IsTemperatureAbsolute = Nullable(true) then {
                                HeatTemp = Temperature e.HeatHoldTemp.Value
                                CoolTemp = Temperature e.CoolHoldTemp.Value
                                Fan = e.Fan
                            }
                        ]
                        StartDate =
                            match DateTime.TryParse($"{e.StartDate} {e.StartTime}") with
                            | (true, x) -> Nullable(x)
                            | (false, _) -> Nullable()
                        EndDate =
                            match DateTime.TryParse($"{e.EndDate} {e.EndTime}") with
                            | (true, x) -> Nullable(x)
                            | (false, _) -> Nullable()
                        Running = e.Running = Nullable(true)
                    }
            ]
        }
    }

    member _.HoldAsync(parameters: TempRange, startTime: DateTime, endTime: DateTime) = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            let ps = new SetHoldParams()

            ps.HeatHoldTemp <- parameters.HeatTemp.Tenths
            ps.CoolHoldTemp <- parameters.CoolTemp.Tenths

            if not (String.IsNullOrEmpty parameters.Fan) then
                ps.Fan <- parameters.Fan

            ps.HoldType <- "dateTime"
            ps.StartDate <- startTime.ToString("yyyy-MM-dd")
            ps.StartTime <- startTime.ToString("HH:mm:ss")
            ps.EndDate <- endTime.ToString("yyyy-MM-dd")
            ps.EndTime <- endTime.ToString("HH:mm:ss")

            yield new SetHoldFunction(Params = ps) :> Function
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    member _.HoldComfortSettingAsync(holdClimateRef: string, startTime: DateTime, endTime: DateTime) = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            let ps = new SetHoldParams()

            ps.HoldClimateRef <- holdClimateRef

            ps.HoldType <- "dateTime"
            ps.StartDate <- startTime.ToString("yyyy-MM-dd")
            ps.StartTime <- startTime.ToString("HH:mm:ss")
            ps.EndDate <- endTime.ToString("yyyy-MM-dd")
            ps.EndTime <- endTime.ToString("HH:mm:ss")

            yield new SetHoldFunction(Params = ps) :> Function
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    member _.CancelHoldAsync() = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            yield new ResumeProgramFunction(Params = new ResumeProgramParams(ResumeAll = false))
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    member _.SendMessageAsync(text: string) = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            yield new SendMessageFunction(Params = new SendMessageParams(Text = text))
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    override _.ToString() = thermostat.Name
