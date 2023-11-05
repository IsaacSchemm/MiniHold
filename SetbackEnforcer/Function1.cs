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
        public async Task Run([TimerTrigger("55 */30 * * * *")]TimerInfo myTimer, ILogger log)
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

                if (!sleep.Active)
                    continue;

                var currentTemp = Temperature.FromFarenheit(
                    Math.Max(
                        info.Runtime.TempRange.HeatTemp.Farenheit,
                        info.Readings.Temperature.Single().Farenheit));
                var desiredTemp = Temperature.FromFarenheit(
                    Math.Min(
                        home.HeatTemp.Farenheit,
                        currentTemp.Farenheit + 1));
                if (desiredTemp.Farenheit >= home.HeatTemp.Farenheit)
                    continue;

                var outdoorTemp = info.Weather.Temperature;
                var compressorMinOutdoorTemp = info.AuxCrossover.Item1;
                if (outdoorTemp.Farenheit < compressorMinOutdoorTemp.Farenheit)
                    continue;

                log.LogInformation($"{sleep.Name} comfort setting is active at {currentTemp}");
                log.LogInformation($"Outdoor temperature {outdoorTemp} is not less than {compressorMinOutdoorTemp}");
                log.LogInformation($"Raising temperature to {desiredTemp}");

                await thermostat.HoldAsync(
                    info.Runtime.TempRange
                        .WithHeatTemp(desiredTemp)
                        .WithCoolTemp(home.CoolTemp),
                    HoldType.NextTransition);
            }
        }
    }
}
