using System.Text;
using WebApplication1;

namespace ClearScriptV8DebugDemo.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            using var client = new V8DebuggerHttpClient(9981);

            var app = builder.Build();

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/debug")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var debugUri = await client.LoadDebugUriAsync();
                        if (debugUri == null)
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        }

                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                        using var websockProxy = new WebsocketProxy();
                        await websockProxy.ConnectAsync(debugUri);
                        await websockProxy.RedirectDataAsync(webSocket, context.RequestAborted);

                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else if (context.Request.Path == "/")
                {
                    try
                    {
                        var debugUri = await client.LoadDebugUriAsync();
                        if (debugUri == null)
                        {
                            await context.Response.BodyWriter.WriteAsync(Encoding.Default.GetBytes("Already debugging"));
                        }
                        else
                        {
                            var localDebugAddr = $"{context.Request.Host}/debug";
                            var devToolsLink = "devtools://devtools/bundled/js_app.html?experiments=true&v8only=true&ws=" + localDebugAddr;
                            await context.Response.BodyWriter.WriteAsync(Encoding.Default.GetBytes($"ws://{context.Request.Host}/debug\n{devToolsLink}"));
                        }
                    }
                    catch
                    {
                        context.Response.StatusCode = StatusCodes.Status418ImATeapot;
                    }
                }
                else
                    await next();
            });

            await app.RunAsync();


        }
    }
}