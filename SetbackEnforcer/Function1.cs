#nullable enable

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
        public async Task Run([TimerTrigger("55 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var client = new Client(Constants.ApiKey, GetTokenAsync, SetTokenAsync);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                var info = await thermostat.GetInformationAsync();
                if (info.Mode != "heat")
                    continue;

                var home = info.ComfortLevels
                    .Where(x => x.Name == "Home")
                    .SingleOrDefault();
                if (home == null)
                    continue;

                var sleep = info.ComfortLevels
                    .Where(x => x.Name == "Sleep")
                    .SingleOrDefault();
                if (sleep == null)
                    continue;

                var setbackEnforcer = info.ComfortLevels
                    .Where(x => x.Name == "DynSetback")
                    .SingleOrDefault();
                if (setbackEnforcer == null)
                    continue;

                if (!setbackEnforcer.Active)
                    continue;

                log.LogInformation($"{setbackEnforcer.Name} comfort setting is active at {info.Runtime.TempRange}");

                var outdoorTemp = info.Weather.Temperature;
                var compressorMinOutdoorTemp = info.AuxCrossover.Item1;

                log.LogInformation($"Comparing outdoor temperature {outdoorTemp} to {compressorMinOutdoorTemp}");

                var desiredComfortLevel = outdoorTemp.Farenheit < compressorMinOutdoorTemp.Farenheit
                    ? sleep
                    : home;

                var desiredRange = new TempRange(
                    heatTemp: desiredComfortLevel.HeatTemp,
                    coolTemp: desiredComfortLevel.CoolTemp,
                    fan: info.Runtime.TempRange.Fan);

                log.LogInformation($"Current: {info.Runtime.TempRange}");
                log.LogInformation($"Desired: {desiredRange}");

                if (!desiredRange.Equals(info.Runtime.TempRange))
                {
                    log.LogInformation($"Setting hold for {desiredRange}");

                    await thermostat.HoldAsync(
                        desiredRange,
                        HoldType.NextTransition);
                }
            }
        }
    }
}
