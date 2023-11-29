namespace MiniHold.Abstractions

open System

module QuickActions =
    let private farenheit = Temperature.FromFarenheit

    let Interval = farenheit 2m

    let DetermineNewRangeToApplyHeat(info: ThermostatInformation) =
        let current = List.max [
            yield! info.Readings.Temperature
            yield info.Runtime.TempRange.HeatTemp
        ]

        { info.Runtime.TempRange with
            HeatTemp = current + Interval
            CoolTemp = current + Interval + farenheit 10.0m }

    let DetermineNewRangeToApplyCool(info: ThermostatInformation) =
        let current = List.min [
            yield! info.Readings.Temperature
            yield info.Runtime.TempRange.CoolTemp
        ]

        { info.Runtime.TempRange with
            HeatTemp = current - Interval - farenheit 10.0m
            CoolTemp = current - Interval }

    let DetermineNewRangeToApplyBackoff(info: ThermostatInformation) =
        let range = info.Runtime.TempRange

        { range with
            HeatTemp = range.HeatTemp - Interval
            CoolTemp = range.CoolTemp + Interval }

    let SetHoldAsync(client: IThermostatClient, tempRange, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldAsync(HoldType.TempRange tempRange, HoldDuration.Range (startTime, endTime))
    }

    let SetFanAsync(client: IThermostatClient, fan, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync(HoldType.TempRange {
            info.Runtime.TempRange with
                Fan = if fan then "on" else "auto"
        }, HoldDuration.Range (startTime, endTime))
    }

    let SetAwayAsync(client: IThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldAsync(HoldType.ComfortLevel "away", HoldDuration.Range (startTime, endTime))
    }

    let SetAwayUntilClockAsync(client: IThermostatClient, timeOfDay: TimeSpan) = task {
        let startTime = client.ToThermostatTime(DateTime.UtcNow)
        let mutable endTime = startTime.Date + timeOfDay
        if endTime < startTime then
            endTime <- endTime.AddDays(1)
        do! client.HoldAsync(HoldType.ComfortLevel "away", HoldDuration.Range (startTime, endTime))
    }