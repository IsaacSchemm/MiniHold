﻿namespace MiniHold.Abstractions

open System

module QuickActions =
    let SetTemperatureOffsetAsync(client: ThermostatClient, offsetFarenheit, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync({
            info.Desired with
                HeatTemp = info.Desired.HeatTemp.AddFarenheit offsetFarenheit
                CoolTemp = info.Desired.CoolTemp.AddFarenheit offsetFarenheit
        }, startTime, endTime)
    }

    let SetFanAsync(client: ThermostatClient, fan, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        let! info = client.GetInformationAsync()
        do! client.HoldAsync({
            info.Desired with
                Fan = if fan then "on" else "auto"
        }, startTime, endTime)
    }

    let SetAwayAsync(client: ThermostatClient, duration) = task {
        let startTime = client.ToThermostatTime(DateTime.Now)
        let endTime = startTime + duration
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }

    let SetAwayUntilTimeAsync(client: ThermostatClient, time: DateTime) = task {
        let startTime = client.ToThermostatTime(DateTime.UtcNow)
        let mutable endTime = startTime.Date + time.TimeOfDay
        if endTime < startTime then
            endTime <- endTime.AddDays(1)
        do! client.HoldComfortSettingAsync("away", startTime, endTime)
    }