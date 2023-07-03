using I8Beef.Ecobee;
using I8Beef.Ecobee.Exceptions;
using I8Beef.Ecobee.Messages;
using MiniHold.Abstractions;

namespace MiniHold.App
{
    public static class ClientStatic
    {
        private static Pin _pin = null;

        public static string EcobeePin => _pin?.EcobeePin;

        public static bool HasToken => _objs.Count > 0;

		public static readonly List<ThermostatObject> _objs = new();

        public static IReadOnlyList<ThermostatObject> ThermostatObjects => _objs;

		public static async Task EstablishThermostatListAsync()
        {
			if (HasToken)
				return;

            var c = new Client(Keys.ApiKey, GetStoredAuthTokenAsync, SetStoredAuthTokenAsync);
            if (await GetStoredAuthTokenAsync() == null)
            {
                _pin ??= await c.GetPinAsync();
            }
            else
            {
                _pin = null;
                await foreach (var tClient in ThermostatEnumerator.FindAsync(c))
                {
                    var x = new ThermostatObject(tClient);
                    await x.Refresh();
                    _objs.Add(x);
                }
            }
        }

        public static async Task EstablishThermostatInformationAsync()
        {
            var invalidBefore = DateTimeOffset.UtcNow - TimeSpan.FromMinutes(10);
            var invalidAfter = DateTimeOffset.UtcNow + TimeSpan.FromMinutes(10);

            foreach (var x in ThermostatObjects)
                if (x.LastUpdated < invalidBefore || x.LastUpdated > invalidAfter)
                    await x.Refresh();
        }

        private static async Task<StoredAuthToken> GetStoredAuthTokenAsync(CancellationToken _ = default)
        {
#if NET8_0_OR_GREATER
            return await SecureStorage.Default.GetAsync("ecobeeToken") is string json
                ? System.Text.Json.JsonSerializer.Deserialize<StoredAuthToken>(json)
                : null;
#else
            return Preferences.Default.Get<string>("ecobeeToken", null) is string xyz
                ? System.Text.Json.JsonSerializer.Deserialize<StoredAuthToken>(xyz)
                : null;
#endif
        }

        private static async Task SetStoredAuthTokenAsync(StoredAuthToken token, CancellationToken _ = default)
        {
#if NET8_0_OR_GREATER
            await SecureStorage.Default.SetAsync("ecobeeToken", System.Text.Json.JsonSerializer.Serialize(token));
#else
            Preferences.Default.Set("ecobeeToken", System.Text.Json.JsonSerializer.Serialize(token));
#endif
        }

        public static async Task GetToken()
        {
            try
            {
                await new Client(Keys.ApiKey, GetStoredAuthTokenAsync, SetStoredAuthTokenAsync).GetAccessTokenAsync(_pin.Code);
                await EstablishThermostatListAsync();
            } catch (ApiAuthException) { }
        }

        public static async Task RemoveToken()
        {
            _objs.Clear();
#if NET8_0_OR_GREATER
            SecureStorage.Default.Remove("ecobeeToken");
#else
            Preferences.Default.Remove("ecobeeToken");
#endif
            await EstablishThermostatListAsync();
        }
    }
}
