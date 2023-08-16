namespace MiniHold.Abstractions

open System
open System.Threading.Tasks

type SimulatedThermostat(name: string) =
    let mutable current: ThermostatInformation = {
        Mode = "auto"
        AuxCrossover = (Temperature 200, Temperature 400)
        CoolDelta = Temperature 5
        HeatDelta = Temperature 5
        ComfortLevels = [
            {
                Ref = "home"
                Name = "Home"
                Active = true
                HeatTemp = Temperature 700
                CoolTemp = Temperature 760
            }
            {
                Ref = "away"
                Name = "Away"
                Active = false
                HeatTemp = Temperature 600
                CoolTemp = Temperature 800
            }
            {
                Ref = "sleep"
                Name = "Sleep"
                Active = false
                HeatTemp = Temperature 660
                CoolTemp = Temperature 740
            }
        ]
        CoolRangeHigh = Temperature 920
        CoolRangeLow = Temperature 650
        HeatRangeHigh = Temperature 790
        HeatRangeLow = Temperature 450
        EquipmentStatus = []
        Runtime = {
            TempRange = {
                HeatTemp = Temperature 700
                CoolTemp = Temperature 760
                Fan = "auto"
            }
            DesiredHumidity = Percentage 36
            DesiredDehumidity = Percentage 60
        }
        Readings = {
            Temperature = [
                Temperature 724
            ]
            Humidity = [
                Percentage 50
            ]
        }
        Sensors = [
            {
                Name = name
                Occupied = true
                Readings = {
                    Temperature = [
                        Temperature 724
                    ]
                    Humidity = [
                        Percentage 50
                    ]
                }
            }
            {
                Name = "Simulated Sensor"
                Occupied = false
                Readings = {
                    Temperature = [
                        Temperature 727
                    ]
                    Humidity = [
                        Percentage 52
                    ]
                }
            }
        ]
        Weather = {
            Condition = "Mostly Clear"
            Temperature = Temperature 664
            Humidity = Percentage 78
        }
        DailyForecasts = [
            {
                Date = new DateTime(2022, 1, 2, 8, 30, 0, DateTimeKind.Local)
                High = Temperature 834
                Low = Temperature 596
                Condition = "Mostly Clear"
                Pop = Percentage 0
            }
            {
                Date = new DateTime(2022, 1, 3, 0, 0, 0, DateTimeKind.Local)
                High = Temperature 898
                Low = Temperature 679
                Condition = "Mostly Clear"
                Pop = Percentage 37
            }
        ]
        Alerts = [
            {
                DateTime = new DateTime(2022, 1, 2, 7, 0, 0, DateTimeKind.Local)
                Text = "This is an example alert."
            }
        ]
        Events = [
            {
                Name = "Example Hold"
                EventType = "hold"
                AbsoluteTemperatureRanges = [
                    {
                        HeatTemp = Temperature 720
                        CoolTemp = Temperature 770
                        Fan = "auto"
                    }
                ]
                StartDate = Nullable(new DateTime(2022, 1, 2, 8, 25, 0, DateTimeKind.Local))
                EndDate = Nullable(new DateTime(2022, 1, 2, 8, 55, 0, DateTimeKind.Local))
                Running = true
            }
        ]
    }

    let toTempRange (c: ComfortLevel) = { HeatTemp = c.HeatTemp; CoolTemp = c.CoolTemp; Fan = "auto" }

    let updateRuntime() =
        let newTempRange =
            match current.Events with
            | active::_ ->
                active.AbsoluteTemperatureRanges.Head
            | [] ->
                current.ComfortLevels |> List.where (fun x -> x.Active) |> List.head |> toTempRange
        current <- { current with Runtime = { current.Runtime with TempRange = newTempRange } }

    override _.ToString() = name

    interface IThermostatClient with
        member _.Name = name

        member _.GetInformationAsync() =
            Task.FromResult(current)

        member _.ToThermostatTime(time) =
            TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local)

        member _.HoldAsync(parameters, startTime, endTime) =
            while not current.Events.IsEmpty && current.Events.Head.EventType = "hold" do
                current <- { current with Events = List.tail current.Events }

            let newHold: Event = {
                Name = "New Example Hold"
                EventType = "hold"
                AbsoluteTemperatureRanges = [parameters]
                StartDate = Nullable startTime
                EndDate = Nullable endTime
                Running = true
            }

            current <- { current with Events = newHold :: current.Events }
            updateRuntime()

            Task.CompletedTask

        member _.CancelHoldAsync() =
            while not current.Events.IsEmpty && current.Events.Head.EventType = "hold" do
                current <- { current with Events = List.tail current.Events }

            updateRuntime()

            Task.CompletedTask

        member _.CreateVacationAsync(name, tempRange, startTime, endTime, fanMinOnTime) =
            let (activeHolds, otherEvents) =
                match current.Events with
                | x::t when x.EventType = "hold" -> [x], t
                | _ -> [], current.Events

            let vacation: Event = {
                Name = name
                EventType = "vacation"
                AbsoluteTemperatureRanges = [tempRange]
                StartDate = Nullable startTime
                EndDate = Nullable endTime
                Running = DateTime.Now > startTime
            }

            current <- { current with Events = activeHolds @ [vacation] @ otherEvents }
            updateRuntime()

            Task.CompletedTask

        member _.DeleteVacationAsync(name) =
            current <- { current with Events = current.Events |> List.where (fun x -> x.EventType <> "vacation" || x.Name <> name) }
            updateRuntime()

            Task.CompletedTask

        member this.HoldComfortSettingAsync(holdClimateRef, startTime, endTime) =
            let climateRef = current.ComfortLevels |> List.where (fun x -> x.Ref = holdClimateRef) |> List.head
            (this :> IThermostatClient).HoldAsync(toTempRange climateRef, startTime, endTime)