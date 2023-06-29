using I8Beef.Ecobee;
using I8Beef.Ecobee.Exceptions;
using I8Beef.Ecobee.Messages;

namespace MiniHold.App
{
    public static class StaticThermostatClient
    {
        private static string _apiKey = null;
        private static Pin _pin = null;
        private static IClient _pendingClient = null;
        private static IClient _activeClient = null;

        public static bool HasApiKey => !string.IsNullOrEmpty(_apiKey);
        public static string EcobeePin => _pin?.EcobeePin;
        public static IClient ActiveClient => _activeClient;

        public static async Task UpdateAsync()
        {
            _apiKey = await SecureStorage.Default.GetAsync("ecobeeApiKey") ?? "";
            _pendingClient = null;
            _pin = null;
            _activeClient = null;

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
                    _activeClient = c;
                }
            }
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
            await SecureStorage.Default.SetAsync("ecobeeApiKey", apiKey);
            await UpdateAsync();
        }

        public static async Task RemoveApiKey()
        {
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
            SecureStorage.Default.Remove("ecobeeToken");
            await UpdateAsync();
        }
    }
}
