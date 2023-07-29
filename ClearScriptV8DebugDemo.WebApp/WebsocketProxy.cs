using System.Net.WebSockets;

namespace WebApplication1
{
    public class WebsocketProxy : IDisposable
    {
        private ClientWebSocket _webSockClient;

        public async Task ConnectAsync(string targetUri)
        {
            _webSockClient = new ClientWebSocket();

            await _webSockClient.ConnectAsync(new Uri(targetUri), default);
        }

        public async Task RedirectDataAsync(WebSocket sock, CancellationToken token)
        {
            await Task.WhenAll(
            Task.Run(async () =>
            {
                var buffer = new byte[1024 * 1024 * 10];
                while (!sock.CloseStatus.HasValue && !_webSockClient.CloseStatus.HasValue)
                {
                    var result = await sock.ReceiveAsync(buffer, token);
                    if (!result.CloseStatus.HasValue)
                        await _webSockClient.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, token);
                }
            }, token),
            Task.Run(async () =>
            {
                var buffer = new byte[1024 * 1024 * 10];
                while (!sock.CloseStatus.HasValue && !_webSockClient.CloseStatus.HasValue)
                {
                    var result = await _webSockClient.ReceiveAsync(buffer, token);
                    if (!result.CloseStatus.HasValue)
                        await sock.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, token);
                }
            }, token));



        }

        public void Dispose()
        {
            _webSockClient?.Dispose();
        }
    }
}
