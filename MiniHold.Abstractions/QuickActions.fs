namespace MiniHold.Abstractions

open System

module QuickActions =
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