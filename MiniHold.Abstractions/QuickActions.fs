namespace MiniHold.Abstractions

open System

module QuickActions =
    let farenheit = Temperature.FromFarenheit

    let DetermineNewRangeToApplyHeat(info: ThermostatInformation) =
        let newSetPoint = List.max [
            for t in info.Readings.Temperature do
                t + info.HeatDelta + farenheit 0.5m
            info.Runtime.TempRange.HeatTemp + farenheit 1.0m
        ]

        {
            info.Runtime.TempRange with
                HeatTemp = newSetPoint
                CoolTemp = newSetPoint + farenheit 10.0m
        }

    let DetermineNewRangeToApplyCool(info: ThermostatInformation) =
        let newSetPoint = List.min [
            for t in info.Readings.Temperature do
                t - info.CoolDelta - farenheit 0.5m
            info.Runtime.TempRange.CoolTemp - farenheit 1.0m
        ]

        {
            info.Runtime.TempRange with
                HeatTemp = newSetPoint - farenheit 10.0m
                CoolTemp = newSetPoint
        }

    let SetHoldAsync(client: IThermostatClient, tempRange, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldAsync(tempRange, startTime, endTime)
    }

    let SetFanAsync(client: IThermostatClient, fan, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync({
            info.Runtime.TempRange with
                Fan = if fan then "on" else "auto"
        }, startTime, endTime)
    }

    let SetAwayAsync(client: IThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }

    let SetAwayUntilClockAsync(client: IThermostatClient, timeOfDay: TimeSpan) = task {
        let startTime = client.ToThermostatTime(DateTime.UtcNow)
        let mutable endTime = startTime.Date + timeOfDay
        if endTime < startTime then
            endTime <- endTime.AddDays(1)
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }