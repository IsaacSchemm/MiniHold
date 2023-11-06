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

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("55 */30 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var client = new Client(Constants.ApiKey, GetTokenAsync, SetTokenAsync);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                var info = await thermostat.GetInformationAsync();

                var activeComfortLevelName = info.ComfortLevels
                    .Where(x => x.Active)
                    .Select(x => x.Name)
                    .FirstOrDefault();

                if (activeComfortLevelName != "DynSetback")
                    continue;

                var manualHoldActive = info.Events
                    .Where(x => x.Running)
                    .Where(x => x.ComfortLevelRef != "home")
                    .Where(x => x.ComfortLevelRef != "sleep")
                    .Any();

                if (manualHoldActive)
                    continue;

                var outdoorTemp = info.Weather.Temperature;
                var compressorMinOutdoorTemp = info.AuxCrossover.Item1;

                string desiredComfortLevelRef = outdoorTemp.Farenheit < compressorMinOutdoorTemp.Farenheit
                    ? "sleep"
                    : "home";

                string activeHoldComfortLevelRef = info.Events
                    .Where(x => x.Running)
                    .Select(x => x.ComfortLevelRef)
                    .FirstOrDefault();

                if (activeHoldComfortLevelRef == desiredComfortLevelRef)
                    continue;

                await thermostat.HoldAsync(
                    HoldType.NewComfortLevel(desiredComfortLevelRef),
                    HoldDuration.NextTransition);
            }
        }
    }
}
