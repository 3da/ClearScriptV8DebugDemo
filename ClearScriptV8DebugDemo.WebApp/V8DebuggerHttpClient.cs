using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClearScriptV8DebugDemo.WebApp
{
    public class V8DebuggerHttpClient : IDisposable
    {
        private readonly int _port;
        private readonly HttpClient _client = new();

        public V8DebuggerHttpClient(int port)
        {
            _port = port;

            _client.Timeout = TimeSpan.FromSeconds(2);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }

        public async Task<string> LoadDebugUriAsync(CancellationToken token = default)
        {
            var options = await _client.GetStringAsync($"http://localhost:{_port}/json", token);

            var qq = JsonConvert.DeserializeObject<JToken>(options);
            return qq[0]["webSocketDebuggerUrl"].ToString();
        }
    }
}
