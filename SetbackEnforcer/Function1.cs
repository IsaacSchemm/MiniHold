using System;
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

        private static readonly Temperature MinAuxSetback = Temperature.FromFarenheit(62);

        private static string GetActiveComfortLevelRef(ThermostatInformation information)
        {
            foreach (var e in information.Events)
                if (e.Running)
                    return e.ComfortLevelRef;
            foreach (var c in information.ComfortLevels)
                if (c.Active)
                    return c.Ref;
            return null;
        }

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 2,24,32 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var client = new Client(Keys.ApiKey, GetTokenAsync, SetTokenAsync);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                // Get current thermostat state
                var info = await thermostat.GetInformationAsync();

                // Get current parameters
                var outdoorTemp = info.Weather.Temperature;
                var compressorMin = info.CompressorProtectionMinTemp;
                var currentRange = info.Runtime.TempRange;

                // If the current setback is already sufficient, keep it
                if (currentRange.HeatTemp.Farenheit <= MinAuxSetback.Farenheit)
                    continue;

                // Check if the conditions are such that aux heat would be used
                bool isAuxHeat = info.Mode switch
                {
                    "auxHeatOnly" => true,
                    "heat" => outdoorTemp.Farenheit <= compressorMin.Farenheit,
                    "auto" => outdoorTemp.Farenheit <= compressorMin.Farenheit,
                    _ => false
                };

                // Determine whether the aux heat will run or not
                if (!isAuxHeat)
                    continue;

                // Calculate desired temperature range
                var newRange = info.Runtime.TempRange.WithHeatTemp(MinAuxSetback);

                // Check whether the sleep comfort setting is active
                if (GetActiveComfortLevelRef(info) != "sleep")
                    continue;

                // Find the active hold, if any
                var activeEvent = info.Events
                    .Where(e => e.Running)
                    .FirstOrDefault();

                var now = thermostat.ToThermostatTime(DateTime.Now);

                // Determine the desired duration of the new hold, based on
                // the duration of the existing hold (if any)
                HoldDuration desiredHoldDuration =
                    activeEvent == null
                        ? HoldDuration.NextTransition
                    : activeEvent.EndDate is DateTime end
                        ? HoldDuration.NewRange(now, end)
                    : HoldDuration.Indefinite;

                await thermostat.HoldAsync(
                    HoldType.NewTempRange(newRange),
                    desiredHoldDuration);
            }
        }
    }
}
