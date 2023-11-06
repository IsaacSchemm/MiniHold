namespace MiniHold.Abstractions

open System
open FSharp.Control
open I8Beef.Ecobee
open I8Beef.Ecobee.Protocol.Objects
open I8Beef.Ecobee.Protocol.Thermostat
open I8Beef.Ecobee.Protocol.Functions
open I8Beef.Ecobee.Protocol
open System.Globalization
open System.Threading.Tasks

[<StructuredFormatDisplay("{PreciseFarenheitString}")>]
type Temperature = Temperature of int
with
    member this.Tenths = let (Temperature x) = this in x

    member this.Farenheit = (decimal this.Tenths / 10m)
    member this.FarenheitString = sprintf "%.0f°F" this.Farenheit
    member this.PreciseFarenheitString = sprintf "%.1f°F" this.Farenheit

    static member FromFarenheit degrees = degrees * 10m |> Math.Round |> int |> Temperature

    static member (+) (Temperature a, Temperature b) = Temperature (a + b)
    static member (-) (Temperature a, Temperature b) = Temperature (a - b)

[<StructuredFormatDisplay("{PercentageString}")>]
type Percentage = Percentage of int
with
    member this.Value = let (Percentage x) = this in x
    member this.PercentageString = sprintf "%d%%" this.Value

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
    member this.ShortDescription = $"{this.HeatTemp}-{this.CoolTemp} (fan: {this.Fan})"
    member this.Contains t = this.HeatTemp < t && t < this.CoolTemp
    interface IUserInterfaceReading with
        member this.Temperatures = ["Heat", this.HeatTemp; "Cool", this.CoolTemp]
        member this.OtherReadings = ["Fan", this.Fan]

type Runtime = {
    TempRange: TempRange
    DesiredHumidity: Percentage
    DesiredDehumidity: Percentage
}

type Readings = {
    Temperature: Temperature list
    Humidity: Percentage list
}
with
    member this.FarenheitString =
        String.concat " / " [for t in this.Temperature do t.FarenheitString]
    member this.PreciseFarenheitString =
        String.concat " / " [for t in this.Temperature do t.PreciseFarenheitString]
    member this.HumidityString =
        String.concat " / " [for h in this.Humidity do h.PercentageString]
    interface IUserInterfaceReading with
        member this.Temperatures = [for x in this.Temperature do "", x]
        member this.OtherReadings = [for x in this.Humidity do "Humidity", x.PercentageString]

type Sensor = {
    Name: string
    Occupied: bool
    Readings: Readings
} with
    member this.NameWithOccupancyIndicator =
        String.concat " " [this.Name; if this.Occupied then "✓"]

type Weather = {
    Condition: string
    Temperature: Temperature
    Humidity: Percentage
}
with
    interface IUserInterfaceReading with
        member this.Temperatures = ["", this.Temperature]
        member this.OtherReadings = ["Condition", this.Condition; "Humidity", this.Humidity.PercentageString]

type DailyForecast = {
    Date: DateTime
    High: Temperature
    Low: Temperature
    Condition: string
    Pop: Percentage
}
with
    interface IUserInterfaceReading with
        member this.Temperatures = [
            "High", this.High
            "Low", this.Low
        ]
        member this.OtherReadings = [
            "Condition", this.Condition
            if this.Pop.Value > 0 then
                "Chance of Precipitation", this.Pop.PercentageString
        ]

type Alert = {
    DateTime: DateTime
    Text: string
}

type Event = {
    Name: string
    EventType: string
    AbsoluteTemperatureRange: TempRange option
    StartDate: Nullable<DateTime>
    EndDate: Nullable<DateTime>
    ComfortLevelRef: string
    Running: bool
} with
    member this.AbsoluteTemperatureRanges =
        [this.AbsoluteTemperatureRange]
        |> List.choose id

    member this.Description = String.concat " " [
        match this.AbsoluteTemperatureRange with
        | None -> ()
        | Some t ->
            $"{t.HeatTemp.FarenheitString}-{t.CoolTemp.FarenheitString}"
            if t.Fan = "on" then
                "(with fan)"
        if not (isNull this.ComfortLevelRef) then
            $"({this.ComfortLevelRef})"
    ]

type ComfortLevel = {
    Ref: string
    Name: string
    Active: bool
    HeatTemp: Temperature
    CoolTemp: Temperature
}

type ThermostatInformation = {
    Mode: string
    AuxCrossover: Temperature * Temperature
    CoolDelta: Temperature
    HeatDelta: Temperature
    ComfortLevels: ComfortLevel list
    CoolRangeHigh: Temperature
    CoolRangeLow: Temperature
    HeatRangeHigh: Temperature
    HeatRangeLow: Temperature
    EquipmentStatus: string list
    Runtime: Runtime
    Readings: Readings
    Sensors: Sensor list
    Weather: Weather
    DailyForecasts: DailyForecast list
    Alerts: Alert list
    Events: Event list
} with
    member this.Heat =
        this.Mode = "auto" || this.Mode = "heat" || this.Mode = "auxHeatOnly"
    member this.Cool =
        this.Mode = "auto" || this.Mode = "cool"
    member this.DisplayMode =
        match this.Mode with
        | "auxHeatOnly" -> "Aux"
        | _ -> CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Mode)
    member this.RuntimeDisplay = {
        new IUserInterfaceReading with
            member _.Temperatures = [
                if this.Heat then "Heat", this.Runtime.TempRange.HeatTemp
                if this.Cool then "Cool", this.Runtime.TempRange.CoolTemp
            ]
            member _.OtherReadings = [
                "Min Humidity", this.Runtime.DesiredHumidity.PercentageString
                "Max Humidity", this.Runtime.DesiredDehumidity.PercentageString
                "Fan", this.Runtime.TempRange.Fan
            ]
    }
    member this.Program =
        this.ComfortLevels
        |> Seq.where (fun x -> x.Active)
        |> Seq.exactlyOne
    member this.ApplyHeatDelta (t: Temperature) =
        t - this.HeatDelta
    member this.ApplyCoolDelta (t: Temperature) =
        t + this.CoolDelta

[<RequireQualifiedAccess>]
type HoldType =
| TempRange of TempRange
| ComfortLevel of string

[<RequireQualifiedAccess>]
type HoldDuration =
| Range of startDate: DateTime * endDate: DateTime
| NextTransition
| Indefinite

type IThermostatClient =
    abstract member Name: string with get
    abstract member ToThermostatTime: time: DateTime -> DateTime
    abstract member GetInformationAsync: unit -> Task<ThermostatInformation>
    abstract member HoldAsync: ``type``: HoldType * duration: HoldDuration -> Task
    abstract member CancelHoldAsync: unit -> Task
    abstract member SetModeAsync: string -> Task
    abstract member CreateVacationAsync: name: string * tempRange: TempRange * startTime: DateTime * endTime: DateTime * fanMinOnTime: int -> Task
    abstract member DeleteVacationAsync: name: string -> Task

type ThermostatClient(client: IClient, thermostat: Thermostat) =
    let timeZone = TimeZoneInfo.FindSystemTimeZoneById(thermostat.Location.TimeZone)

    override _.ToString() = thermostat.Name

    interface IThermostatClient with
        member _.Name = thermostat.Name

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
                AuxCrossover = (Temperature t.Settings.CompressorProtectionMinTemp.Value, Temperature t.Settings.AuxMaxOutdoorTemp.Value)
                CoolDelta = Temperature t.Settings.Stage1CoolingDifferentialTemp.Value
                HeatDelta = Temperature t.Settings.Stage1HeatingDifferentialTemp.Value
                ComfortLevels = [
                    for c in t.Program.Climates do
                        {
                            Ref = c.ClimateRef
                            Name = c.Name
                            Active = t.Program.CurrentClimateRef = c.ClimateRef
                            HeatTemp = Temperature c.HeatTemp.Value
                            CoolTemp = Temperature c.CoolTemp.Value
                        }
                ]
                CoolRangeHigh = Temperature t.Settings.CoolRangeHigh.Value
                CoolRangeLow = Temperature t.Settings.CoolRangeLow.Value
                HeatRangeHigh = Temperature t.Settings.HeatRangeHigh.Value
                HeatRangeLow = Temperature t.Settings.HeatRangeLow.Value
                EquipmentStatus = t.EquipmentStatus.Split(',') |> Seq.except [""] |> Seq.toList
                Runtime = {
                    TempRange = {
                        HeatTemp = Temperature t.Runtime.DesiredHeat.Value
                        CoolTemp = Temperature t.Runtime.DesiredCool.Value
                        Fan = t.Runtime.DesiredFanMode
                    }
                    DesiredHumidity = Percentage t.Runtime.DesiredHumidity.Value
                    DesiredDehumidity = Percentage t.Runtime.DesiredDehumidity.Value
                }
                Readings = {
                    Temperature = [
                        if t.Runtime.ActualTemperature.HasValue then
                            Temperature t.Runtime.ActualTemperature.Value
                    ]
                    Humidity = [
                        if t.Runtime.ActualHumidity.HasValue then
                            Percentage t.Runtime.ActualHumidity.Value
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
                                        Percentage (Int32.Parse c.Value)
                            ]
                        }
                    }
                ]
                Weather = {
                    Condition = currentWeather.Condition
                    Temperature = Temperature currentWeather.Temperature.Value
                    Humidity = Percentage currentWeather.RelativeHumidity.Value
                }
                DailyForecasts = [
                    for w in t.Weather.Forecasts do
                        if w.TempHigh <> Nullable -5002 && w.TempLow <> Nullable -5002 then
                            {
                                Date = DateTime.Parse(w.DateTime, CultureInfo.InvariantCulture)
                                High = Temperature w.TempHigh.Value
                                Low = Temperature w.TempLow.Value
                                Condition = w.Condition
                                Pop = Percentage w.Pop.Value
                            }
                ]
                Alerts = [
                    for a in t.Alerts do {
                        DateTime = DateTime.Parse($"{a.Date} {a.Time}", CultureInfo.InvariantCulture)
                        Text = a.Text
                    }
                ]
                Events = [
                    for e in t.Events do
                        {
                            Name = e.Name
                            EventType = e.Type
                            AbsoluteTemperatureRange =
                                if e.IsTemperatureAbsolute = Nullable(true)
                                then Some {
                                    HeatTemp = Temperature e.HeatHoldTemp.Value
                                    CoolTemp = Temperature e.CoolHoldTemp.Value
                                    Fan = e.Fan
                                }
                                else None
                            StartDate =
                                match DateTime.TryParse($"{e.StartDate} {e.StartTime}") with
                                | (true, x) -> Nullable(x)
                                | (false, _) -> Nullable()
                            EndDate =
                                match DateTime.TryParse($"{e.EndDate} {e.EndTime}") with
                                | (true, x) -> Nullable(x)
                                | (false, _) -> Nullable()
                            ComfortLevelRef = e.HoldClimateRef
                            Running = e.Running = Nullable(true)
                        }
                ]
            }
        }

        member _.HoldAsync(holdType: HoldType, holdDuration: HoldDuration) = task {
            let request = new ThermostatUpdateRequest()
            request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
            request.Functions <- [|
                let ps = new SetHoldParams()

                match holdType with
                | HoldType.TempRange parameters ->
                    ps.HeatHoldTemp <- parameters.HeatTemp.Tenths
                    ps.CoolHoldTemp <- parameters.CoolTemp.Tenths

                    if not (String.IsNullOrEmpty parameters.Fan) then
                        ps.Fan <- parameters.Fan
                | HoldType.ComfortLevel holdClimateRef ->
                    ps.HoldClimateRef <- holdClimateRef

                match holdDuration with
                | HoldDuration.Range (startTime, endTime) ->
                    ps.HoldType <- "dateTime"
                    ps.StartDate <- startTime.ToString("yyyy-MM-dd")
                    ps.StartTime <- startTime.ToString("HH:mm:ss")
                    ps.EndDate <- endTime.ToString("yyyy-MM-dd")
                    ps.EndTime <- endTime.ToString("HH:mm:ss")
                | HoldDuration.NextTransition ->
                    ps.HoldType <- "nextTransition"
                | HoldDuration.Indefinite ->
                    ps.HoldType <- "indefinite"

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

        member _.SetModeAsync(mode: string) = task {
            let request = new ThermostatUpdateRequest()
            request.Selection <- new Selection(SelectionType = "thermostats", SelectionMatch = thermostat.Identifier)
            request.Thermostat <- {|
                settings = {|
                    hvacMode = mode
                |}
            |}
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