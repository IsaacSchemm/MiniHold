using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using I8Beef.Ecobee;
using Micromanager.Logic;
using Microsoft.Azure.Functions.Worker;
using MiniHold.Abstractions;
using Newtonsoft.Json;

namespace Micromanager
{
    public class Function1
    {
        private async Task<StoredAuthToken> GetTokenAsync(CancellationToken cancellationToken)
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(Keys.VaultUri, credential);
            var response = await client.GetSecretAsync("ecobeeJson", cancellationToken: cancellationToken);
            string json = response.Value.Value;
            return JsonConvert.DeserializeObject<StoredAuthToken>(json);
        }

        private async Task SetTokenAsync(StoredAuthToken token, CancellationToken cancellationToken)
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(Keys.VaultUri, credential);
            await client.SetSecretAsync("ecobeeJson", JsonConvert.SerializeObject(token), cancellationToken);
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 1 */3 * * *")] TimerInfo myTimer)
        {
            var client = new Client(Keys.EcobeeApiKey, GetTokenAsync, SetTokenAsync);
            //var pin = await client.GetPinAsync();
            //await client.GetAccessTokenAsync(pin.Code);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                var info = await thermostat.GetInformationAsync();

                if (info.Mode != "heat" && info.Mode != "auxHeatOnly")
                    continue;

                var estimates = Algorithm.EstimateCarbonUsage(
                    await ElectricityMap.GetCarbonIntensityAsync(
                        new ElectricityMapOptions(
                            Keys.ElectricityMapApiKey,
                            Keys.ElectricityMapZone)),
                    (double)info.Weather.Temperature.Farenheit);

                string newMode = estimates.furnaceCarbonIntensity < estimates.heatPumpCarbonIntensity
                    ? "auxHeatOnly"
                    : "heat";

                if (newMode != info.Mode)
                    await thermostat.SetModeAsync(newMode);
            }
        }
    }
}
