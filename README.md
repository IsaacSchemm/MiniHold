# MiniHold

This repository contains:

* **MiniHold.Abstractions**: an F# abstraction over the `I8Beef.Ecobee`
  library used to connect to the ecobee API.
* **MiniHold.Forms**: A VB.NET WinForms app.
* **MiniHold.App**: A .NET MAUI Blazor app for Windows and Android.

Both apps offer the following functions:

* View the current outdoor temperature (from the forecasting service), the
  current indoor temperature, and the thermostat's current setting
* View the thermostat's current base program and the current or next hold
  event (if any)
* Set a 10-minute or 30-minute hold to raise or lower the temperature range by
  3°F, or to force the fan to run
* Set a hold on the Away comfort setting for 10 minutes, 30 minutes, 2 hours,
  4 hours, 6 hours, or until 7:00 AM, 4:00 PM, or 9:00 PM (according to the
  thermostat's time zone)
* View the abstraction of the thermostat's current state provided by the
  underlying F# code

This app currently assumes your thermostat is set to Auto, and not to Cool,
Heat, Aux, or Off. It also assumes your ecobee account has developer status
and that you've created an app integration.
