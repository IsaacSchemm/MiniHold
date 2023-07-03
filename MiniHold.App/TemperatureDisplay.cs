using MiniHold.Abstractions;

namespace MiniHold.App
{
    public record TemperatureDisplay(Temperature Heat, Temperature Cool, Humidity Humidity, string Condition, string HeatFan, string CoolFan)
    {
        public TemperatureDisplay(Weather w)
            : this(w.Temperature, w.Temperature, w.Humidity, w.Condition, null, null) { }

        public TemperatureDisplay(Readings r)
            : this(r.Temperature.Head, r.Temperature.Head, r.Humidity.Head, null, null, null) { }

        public TemperatureDisplay(TempRange tr)
            : this(tr.HeatTemp, tr.CoolTemp, null, null, tr.Fan, tr.Fan) { }

        public TemperatureDisplay(Program p)
            : this(p.HeatTemp, p.CoolTemp, null, p.Name, p.HeatFan, p.CoolFan) { }
    }
}
