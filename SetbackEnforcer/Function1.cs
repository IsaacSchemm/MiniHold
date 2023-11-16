using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using I8Beef.Ecobee;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MiniHold.Abstractions;
using Newtonsoft.Json;

namespace SetbackEnforcer
{
    public class Function1
    {
        private async Task<StoredAuthToken> GetTokenAsync(CancellationToken cancellationToken)
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(Constants.VaultUri, credential);
            var response = await client.GetSecretAsync("ecobeeJson", cancellationToken: cancellationToken);
            string json = response.Value.Value;
            return JsonConvert.DeserializeObject<StoredAuthToken>(json);
        }

        private async Task SetTokenAsync(StoredAuthToken token, CancellationToken cancellationToken)
        {
            var credential = new DefaultAzureCredential();
            var client = new SecretClient(Constants.VaultUri, credential);
            await client.SetSecretAsync("ecobeeJson", JsonConvert.SerializeObject(token), cancellationToken);
        }

        private static readonly Temperature MinAuxSetback = Temperature.FromFarenheit(62);

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 2,32 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var client = new Client(Constants.ApiKey, GetTokenAsync, SetTokenAsync);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                // Get current thermostat state
                var info = await thermostat.GetInformationAsync();

                // Find comfort setting used by this function
                var customComfortSetting = info.ComfortLevels
                    .Where(x => x.Ref == "sleep")
                    .FirstOrDefault();

                // Skip if the Sleep comfort setting is missing or not currently active
                if (customComfortSetting == null || !customComfortSetting.Active)
                    continue;

                // Skip if a hold is set (either by this function or by the user)
                bool manualHoldActive = info.Events
                    .Where(x => x.Running)
                    .Any();
                if (manualHoldActive)
                    continue;

                // Determine whether the aux heat will run or not
                var outdoorTemp = info.Weather.Temperature;
                var compressorMinOutdoorTemp = info.AuxCrossover.Item1;

                bool isAuxHeat = info.Mode switch
                {
                    "aux" => true,
                    "heat" => outdoorTemp.Farenheit <= compressorMinOutdoorTemp.Farenheit,
                    "auto" => outdoorTemp.Farenheit <= compressorMinOutdoorTemp.Farenheit,
                    _ => false
                };

                // If the aux heat is not needed, keep the current setpoint
                if (!isAuxHeat)
                    continue;

                // Check current thermostat setting
                var currentRange = info.Runtime.TempRange;

                // If the current setback is already sufficient, keep it
                if (currentRange.HeatTemp.Farenheit <= MinAuxSetback.Farenheit)
                    continue;

                // Determine new temperature range
                var newRange = info.Runtime.TempRange.WithHeatTemp(MinAuxSetback);

                // Set hold for new temperature range
                await thermostat.HoldAsync(
                    HoldType.NewComfortLevel(customComfortSetting.Ref),
                    HoldDuration.NextTransition);
            }
        }
    }
}
