using I8Beef.Ecobee;
using I8Beef.Ecobee.Exceptions;
using I8Beef.Ecobee.Messages;
using MiniHold.Abstractions;

namespace MiniHold.App
{
    public static class ClientStatic
    {
        private static string _apiKey = null;
        private static Pin _pin = null;
        private static IClient _pendingClient = null;

        public static bool HasApiKey => !string.IsNullOrEmpty(_apiKey);
        public static string EcobeePin => _pin?.EcobeePin;

        public static bool HasToken { get; private set; } = false;

        public static readonly List<ThermostatObject> _objs = new();

        public static IReadOnlyList<ThermostatObject> ThermostatObjects => _objs;

        public static int Busy { get; set; } = 0;

        public static async Task UpdateAsync()
        {
            if (HasToken)
                return;

            Busy++;

            _apiKey = await SecureStorage.Default.GetAsync("ecobeeApiKey") ?? "";
            _pendingClient = null;
            _pin = null;
            _objs.Clear();

            if (HasApiKey)
            {
                var c = new Client(_apiKey, GetStoredAuthTokenAsync, SetStoredAuthTokenAsync);
                if (await GetStoredAuthTokenAsync() == null)
                {
                    _pin = await c.GetPinAsync();
                    _pendingClient = c;
                }
                else
                {
                    HasToken = true;
                    await foreach (var tClient in ThermostatClient.GetAllAsync(c))
                    {
                        var x = new ThermostatObject(tClient);
                        await x.Refresh();
                        _objs.Add(x);
                    }
                }
            }

            Busy--;
        }

        private static async Task<StoredAuthToken> GetStoredAuthTokenAsync(CancellationToken _ = default)
        {
            return await SecureStorage.Default.GetAsync("ecobeeToken") is string json
                ? System.Text.Json.JsonSerializer.Deserialize<StoredAuthToken>(json)
                : null;
        }

        private static async Task SetStoredAuthTokenAsync(StoredAuthToken token, CancellationToken _ = default)
        {
            await SecureStorage.Default.SetAsync("ecobeeToken", System.Text.Json.JsonSerializer.Serialize(token));
        }

        public static async Task SetApiKey(string apiKey)
        {
            HasToken = false;
            await SecureStorage.Default.SetAsync("ecobeeApiKey", apiKey);
            await UpdateAsync();
        }

        public static async Task RemoveApiKey()
        {
            HasToken = false;
            SecureStorage.Default.Remove("ecobeeApiKey");
            await UpdateAsync();
        }

        public static async Task GetToken()
        {
            try
            {
                await _pendingClient.GetAccessTokenAsync(_pin.Code);
                await UpdateAsync();
            } catch (ApiAuthException) { }
        }

        public static async Task RemoveToken()
        {
            _objs.Clear();
            SecureStorage.Default.Remove("ecobeeToken");
            await UpdateAsync();
        }
    }
}
