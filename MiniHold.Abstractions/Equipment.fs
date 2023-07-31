namespace MiniHold.Abstractions

type Equipment = Equipment of string
with
    member this.Value =
        let (Equipment str) = this
        str
    member this.Name =
        match this.Value with
            | "heatPump" -> "Heat 1 (A/C)"
            | "heatPump2" -> "Heat 2 (A/C)"
            | "heatPump3" -> "Heat 3 (A/C)"
            | "compCool1" -> "Cool 1 (A/C)"
            | "compCool2" -> "Cool 2 (A/C)"
            | "auxHeat" -> "Heat 1 (Aux)"
            | "auxHeat2" -> "Heat 2 (Aux)"
            | "auxHeat3" -> "Heat 3 (Aux)"
            | "fan" -> "Fan"
            | "humidifier" -> "Humidifier"
            | "dehumidifier" -> "Dehumidifier"
            | x -> x
    member this.Heat =
        this.Value.StartsWith("heatPump") || this.Value.StartsWith("auxHeat")
    member this.Cool =
        this.Value.StartsWith("compCool")
    member this.Comp =
        this.Value.StartsWith("heatPump") || this.Value.StartsWith("compCool")
    member this.AuxHeat =
        this.Value.StartsWith("auxHeat")

module Equipment =
    let FromThermostatInformation ti = [for str in ti.EquipmentStatus do Equipment str]
