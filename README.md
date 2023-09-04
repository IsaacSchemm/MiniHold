# MiniHold

A .NET MAUI / Blazor app for Windows and Android, for use with ecobee
thermostats.

I don't plan on distributing binaries of this app, but you can compile it
yourself with Visual Studio 2022 and its mobile app development workload. You
will need to create the module `Keys.fs` in the MiniHold.Abstractions project
(it should be the first file in the project) and enter the API key you've
generated as an ecobee developer:

    namespace MiniHold.Abstractions

    module Keys =
        let ApiKey = "key_goes_here"

The main app project is **MiniHold.App**, which is set up to run on both
Windows 10+ and Android. The easiest way to build and install the app is to
debug it from Visual Studio - for Android, this means enabling USB debugging
on your device and using Visual Studio to debug the app remotely on it.

Also available is **MiniHold.Tray**, a VB.NET WinForms application that can be
minimized to the system tray, shows the current indoor and outdoor temperature
in a tooltip, and lets you set certain short-term holds.

## Display

* **Thermostat name**
* **Mode and equipment status**: The current mode of the thermostat (auto,
  heat, cool, aux, or off) and any running equipment (e.g. "Cool 1 (A/C)",
  "Heat 1 (Aux)", "Fan").
* **Current temperature**: The indoor temperature (rounded to the nearest
  degree Farenheit) and humidity percentage.
* **Last updated**: The last time MiniHold got new information from the ecobee
  servers.
* **Weather**: The current outdoor temperature, humidity, etc., according to
  the weather service used by ecobee.
* **Forecast**: The high and low temperature for the current calendar day.
* **Runtime**: The current set points of the thermostat for both heating and
  cooling (only the Auto mode will use both set points, but the thermostat
  itself always tracks them), the set humidity range, and whether or not the
  fan is set to always run.
* **Hold**: The currently active hold (or vacation, etc.), and its start and
  end date/time.
* **Sensors**: A list of temperature and occupancy readings for the thermostat
  and each SmartSensor, as well as the overall temperature reading as averaged
  by the thermostat based on its algorithms.
* **Comfort Settings**: A list of each comfort setting and its corresponding
  temperature range, along with the temperature at which the thermostat will
  engage heating and cooling (after taking the configured delta into account).
  Also includes a Runtime row for the current thermostat setting.

## Functions

* **15m Hold** and **30m Hold**: Set a hold for 15 or 30 minutes, 
  respectively.
  * **Heat**: Adjusts the set point for heating 1.5°F past the point which
    causes the thermostat to activate heating, given the current indoor
    temperature. The cool point will be set 10°F above the heat point.
  * **Cool**: Adjusts the set point for cooling 1.5°F past the point which
    causes the thermostat to activate cooling, given the current indoor
    temperature. The heat point will be set 10°F below the heat point.
  * **Fan**: Sets a hold that forces the fan on while using the same set points
    as the current runtime.
* **Away**: Set a hold to apply the Away comfort setting for a certain amount
  of time.
  * **For 1 hour**: Sets away for 60 minutes. (This is a quick way to
    essentially pause your heating/cooling equipment to reduce noise, without
    the risk of forgetting to turn it back on.)
  * **For 1 day**: Sets away for 24 hours.
  * **For 1 week**: Sets away for 7 days. (Remember that you can cancel the
    active hold before it expires.)
  * **until 7 am**, **until 4 pm**, **until 9 pm**: Sets away until the next
    time the clock reads a certain time, which could be on the current
    calendar day or on the next one. (This may be more useful with auxiliary
    heat, which is generally consistent, than with heat pumps, where output
    varies depending on the temperature delta between indoors and outdoors.)
* **Cancel Hold**: Only shows up if a hold is active. Removes it from the
  event stack and returns to the program / vacation / etc.

Vacations can be scheduled and deleted from the Vacations page. The app also
includes a Forecast page that shows daily forecasts from the weather service
used by the thermostat and an Abstraction page that shows the underlying
`ThermostatInformation` F# record that the app puts together.
