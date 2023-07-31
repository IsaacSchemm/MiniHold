namespace MiniHold.Abstractions

open System

module QuickActions =
    let SetTemperatureOffsetAsync(client: ThermostatClient, offsetFarenheit, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync({
            info.Runtime.TempRange with
                HeatTemp = info.Runtime.TempRange.HeatTemp + Temperature.FromFarenheit offsetFarenheit
                CoolTemp = info.Runtime.TempRange.CoolTemp + Temperature.FromFarenheit offsetFarenheit
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