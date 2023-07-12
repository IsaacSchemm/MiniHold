namespace MiniHold.Abstractions

open System
open FSharp.Control
open I8Beef.Ecobee
open I8Beef.Ecobee.Protocol.Objects
open I8Beef.Ecobee.Protocol.Thermostat
open I8Beef.Ecobee.Protocol.Functions
open I8Beef.Ecobee.Protocol

[<StructuredFormatDisplay("{PreciseFarenheitString}")>]
type Temperature = Temperature of int
with
    member this.Tenths = let (Temperature x) = this in x
    member this.Farenheit = (decimal this.Tenths / 10m)
    member this.FarenheitString = sprintf "%.0f°F" this.Farenheit
    member this.PreciseFarenheitString = sprintf "%.1f°F" this.Farenheit

    static member FromFarenheit degrees = degrees * 10m |> Math.Round |> int |> Temperature

    member this.AddFarenheit degrees = Temperature.FromFarenheit (this.Farenheit + degrees)

[<StructuredFormatDisplay("{PercentageString}")>]
type Humidity = Humidity of int
with
    member this.PercentageString = let (Humidity x) = this in sprintf "%d%%" x

type IUserInterfaceReading =
    abstract member Temperatures: (string * Temperature) list
    abstract member OtherReadings: (string * string) list

type TempRange = {
    HeatTemp: Temperature
    CoolTemp: Temperature
    Fan: string
} with
    member this.WithHeatTemp x = { this with HeatTemp = x }
    member this.WithCoolTemp x = { this with CoolTemp = x }
    member this.WithFan x = { this with Fan = x }
    interface IUserInterfaceReading with
        member this.Temperatures = ["Heat", this.HeatTemp; "Cool", this.CoolTemp]
        member this.OtherReadings = ["Fan", this.Fan]

type Readings = {
    Temperature: Temperature list
    Humidity: Humidity list
}
with
    interface IUserInterfaceReading with
        member this.Temperatures = [for x in this.Temperature do "", x]
        member this.OtherReadings = [for x in this.Humidity do "Humidity", x.PercentageString]

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
with
    interface IUserInterfaceReading with
        member this.Temperatures = ["", this.Temperature]
        member this.OtherReadings = ["Condition", this.Condition; "Humidity", this.Humidity.PercentageString]

type Program = {
    Name: string
    CoolTemp: Temperature
    HeatTemp: Temperature
    CoolFan: string
    HeatFan: string
}
with
    interface IUserInterfaceReading with
        member this.Temperatures = ["Heat", this.HeatTemp; "Cool", this.CoolTemp]
        member this.OtherReadings = ["Comfort Setting", this.Name; "Fan (heat)", this.HeatFan; "Fan (cool)", this.CoolFan]

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
    Name: string
    EventType: string
    AbsoluteTemperatureRanges: TempRange list
    StartDate: Nullable<DateTime>
    EndDate: Nullable<DateTime>
    Running: bool
}

type ThermostatInformation = {
    Mode: string
    CoolRangeHigh: Temperature
    CoolRangeLow: Temperature
    HeatRangeHigh: Temperature
    HeatRangeLow: Temperature
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

    member _.GetInformationAsync() = task {
        let request = new ThermostatRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Selection.IncludeAlerts <- true
        request.Selection.IncludeEquipmentStatus <- true
        request.Selection.IncludeEvents <- true
        request.Selection.IncludeProgram <- true
        request.Selection.IncludeRuntime <- true
        request.Selection.IncludeSettings <- true
        request.Selection.IncludeSensors <- true
        request.Selection.IncludeWeather <- true
        let! response = client.GetAsync<ThermostatRequest, ThermostatResponse>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
        let t = Seq.exactlyOne response.ThermostatList
        let currentWeather = Seq.head t.Weather.Forecasts
        return {
            Mode = t.Settings.HvacMode
            CoolRangeHigh = Temperature t.Settings.CoolRangeHigh.Value
            CoolRangeLow = Temperature t.Settings.CoolRangeLow.Value
            HeatRangeHigh = Temperature t.Settings.HeatRangeHigh.Value
            HeatRangeLow = Temperature t.Settings.HeatRangeLow.Value
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
                            CoolFan = c.CoolFan
                            HeatFan = c.HeatFan
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
                        Name = e.Name
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

    member _.CreateVacationAsync(name: string, tempRange: TempRange, startTime: DateTime, endTime: DateTime, fanMinOnTime: int) = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            let ps = new CreateVacationParams()

            ps.Name <- name
            ps.CoolHoldTemp <- tempRange.CoolTemp.Tenths
            ps.HeatHoldTemp <- tempRange.HeatTemp.Tenths
            ps.StartDate <- startTime.ToString("yyyy-MM-dd")
            ps.StartTime <- startTime.ToString("HH:mm:ss")
            ps.EndDate <- endTime.ToString("yyyy-MM-dd")
            ps.EndTime <- endTime.ToString("HH:mm:ss")
            ps.Fan <- tempRange.Fan
            ps.FanMinOnTime <- sprintf "%d" fanMinOnTime

            yield new CreateVacationFunction(Params = ps)
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    member _.DeleteVacationAsync(name: string) = task {
        let request = new ThermostatUpdateRequest()
        request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
        request.Functions <- [|
            yield new DeleteVacationFunction(Params = new DeleteVacationParams(Name = name))
        |]
        let! response = client.PostAsync<ThermostatUpdateRequest, Response>(request)
        if response.Status.Code.HasValue && response.Status.Code.Value <> 0 then
            failwithf "%d: %s" response.Status.Code.Value response.Status.Message
    }

    override _.ToString() = thermostat.Name
