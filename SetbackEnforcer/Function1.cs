#nullable enable

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
            await client.SetSecretAsync("ecobeeJson", JsonConvert.SerializeObject(token));
        }

        private static readonly string ComfortLevelName = "Sleep";
        private static readonly Temperature SetbackTemperature = Temperature.FromFarenheit(65);

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("33 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var client = new Client(Constants.ApiKey, GetTokenAsync, SetTokenAsync);
            await foreach (var thermostat in ThermostatEnumerator.FindAsync(client))
            {
                var info = await thermostat.GetInformationAsync();
                var active = info.ComfortLevels
                    .Where(x => x.Active)
                    .SingleOrDefault();
                if (active != null && active.Name == ComfortLevelName)
                {
                    var heatTemp = info.Runtime.TempRange.HeatTemp;
                    var outdoorTemp = info.Weather.Temperature;
                    var compressorMinOutdoorTemp = info.AuxCrossover.Item1;
                    if (heatTemp.Farenheit > SetbackTemperature.Farenheit)
                    {
                        log.LogInformation($"{active.Name} comfort setting is active at {heatTemp}");
                        if (outdoorTemp.Farenheit < compressorMinOutdoorTemp.Farenheit)
                        {
                            log.LogInformation($"Outdoor temperature {outdoorTemp} is less than {compressorMinOutdoorTemp}");
                            log.LogInformation($"Setting a hold for {SetbackTemperature}");

                            await thermostat.HoldAsync(
                                info.Runtime.TempRange.WithHeatTemp(SetbackTemperature),
                                HoldType.NextTransition);
                        }
                    }
                }
            }
        }
    }
}
