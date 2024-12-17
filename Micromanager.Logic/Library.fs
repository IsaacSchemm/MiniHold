namespace Micromanager.Logic

open System
open System.Net.Http
open System.Net.Http.Json
open MathNet.Numerics

[<Measure>] type gramsCO2
[<Measure>] type kWh_elec
[<Measure>] type kWh_heat

type ElectricityMapOptions = {
    apiKey: string
    zone: string
}

module ElectricityMap =
    let private client = lazy (new HttpClient())

    type ResponseBody = {
        carbonIntensity: float
    }

    let GetCarbonIntensityAsync options = task {
        use req = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.electricitymap.org/v3/carbon-intensity/latest?zone={Uri.EscapeDataString(options.zone)}")
        req.Headers.Add("auth-token", options.apiKey)
        use! resp = client.Value.SendAsync(req)
        let! obj = resp.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<ResponseBody>()
        return obj.carbonIntensity
    }

module Algorithm =
    let EstimateCarbonUsage gridCarbonIntensity outdoorTemperature =
        let furnaceEfficiency = 0.965

        let naturalGasHeat = 185.0<gramsCO2/kWh_heat>

        let getCurrentCOP temp =
            let func = Fit.LineFunc(
                [| 47.0; 17.0; 05.0 |],
                [| 3.60; 2.50; 2.00 |])

            func.Invoke(temp) |> LanguagePrimitives.FloatWithMeasure<kWh_heat / kWh_elec>

        let currentCOP = getCurrentCOP outdoorTemperature

        {|
            heatPumpCarbonIntensity = gridCarbonIntensity / currentCOP
            furnaceCarbonIntensity = naturalGasHeat * furnaceEfficiency
        |}
