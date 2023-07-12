using I8Beef.Ecobee;
using I8Beef.Ecobee.Exceptions;
using I8Beef.Ecobee.Messages;
using MiniHold.Abstractions;

namespace MiniHold.App
{
    public static class ClientStatic
    {
        private static readonly SemaphoreSlim _establishListSem = new(1, 1);

        private static Pin _pin = null;

        public static string EcobeePin => _pin?.EcobeePin;

        public static bool HasToken => _objs.Count > 0;

		public static readonly List<ThermostatObject> _objs = new();

        public static IReadOnlyList<ThermostatObject> ThermostatObjects => _objs;

		public static async Task EstablishThermostatListAsync()
        {
            await _establishListSem.WaitAsync();

            try
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
            finally
            {
                _establishListSem.Release();
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
            try
            {
                return await SecureStorage.Default.GetAsync("ecobeeToken") is string json
                    ? System.Text.Json.JsonSerializer.Deserialize<StoredAuthToken>(json)
                    : null;
            }
            catch (Exception)
            {
                SecureStorage.Default.RemoveAll();
                return null;
            }
        }

        private static async Task SetStoredAuthTokenAsync(StoredAuthToken token, CancellationToken _ = default)
        {
            await SecureStorage.Default.SetAsync("ecobeeToken", System.Text.Json.JsonSerializer.Serialize(token));
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
            SecureStorage.Default.Remove("ecobeeToken");
            await EstablishThermostatListAsync();
        }
    }
}
