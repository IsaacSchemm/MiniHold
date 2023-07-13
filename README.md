# MiniHold

A .NET MAUI / Blazor app for Windows and Android, for use with ecobee
thermostats.

* View the current outdoor temperature (from the forecasting service), the
  current indoor temperature, and the thermostat's current setting
* View the thermostat's current base program and the current or next hold
  event (if any)
* View the daily weather forecast from the ecobee web service
* Set a 10-minute or 30-minute hold to raise or lower the temperature range by
  2°F, or to force the fan to run
* Set a hold on the Away comfort setting in short-term intervals, for the next
  week, or until the clock reads 7:00 AM, 4:00 PM, or 9:00 PM (according to
  the thermostat's time zone)
* View, create, and delete vacation parameters (minimum fan runtime not
  currently supported)
* View the abstraction of the thermostat's current state provided by the
  underlying F# code

To compile, create the module `Keys.fs` in the Abstractions project, and enter
your API key as a constant:

    namespace MiniHold.Abstractions

    module Keys =
        let ApiKey = "key_goes_here"

The main app project is **MiniHold.App**. Easiest way to build for Android is
by enabling USB debugging on your device and using Visual Studio to debug the
app remotely on it.

There's also a VB.NET WinForms app, **MiniHold.Forms**, with many of the same
features, but this is not as actively maintained.
