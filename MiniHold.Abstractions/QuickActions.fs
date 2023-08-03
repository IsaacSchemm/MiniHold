namespace MiniHold.Abstractions

open System

module QuickActions =
    let SetHeatAsync(client: ThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        let newSetPoint = List.head info.Readings.Temperature + info.HeatDelta + Temperature.FromFarenheit 0.5m
        do! client.HoldAsync({
            info.Runtime.TempRange with
                HeatTemp = newSetPoint
                CoolTemp = newSetPoint + Temperature.FromFarenheit 5m
        }, startTime, endTime)
    }

    let SetCoolAsync(client: ThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        let newSetPoint = List.head info.Readings.Temperature - info.CoolDelta - Temperature.FromFarenheit 0.5m
        do! client.HoldAsync({
            info.Runtime.TempRange with
                HeatTemp = newSetPoint - Temperature.FromFarenheit 5m
                CoolTemp = newSetPoint
        }, startTime, endTime)
    }

    let SetFanAsync(client: ThermostatClient, fan, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync({
            info.Runtime.TempRange with
                Fan = if fan then "on" else "auto"
        }, startTime, endTime)
    }

    let SetAwayAsync(client: ThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }

    let SetAwayUntilClockAsync(client: ThermostatClient, timeOfDay: TimeSpan) = task {
        let startTime = client.ToThermostatTime(DateTime.UtcNow)
        let mutable endTime = startTime.Date + timeOfDay
        if endTime < startTime then
            endTime <- endTime.AddDays(1)
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }