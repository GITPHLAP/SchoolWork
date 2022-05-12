using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System;
using Microsoft.Extensions.Logging;
using WS_Server.DAL;
using Newtonsoft.Json;

namespace WS_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {

        private readonly ILogger _logger;

        public WebSocketController(ILogger<WebSocketController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/send")]
        public async Task Get()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                // AcceptWebSocketAsync() upgrade the normal TCP connection to a websocket and return a websocket object 
                // Which can be used to send or receive msg
                using (WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                        await Send(HttpContext,
                            webSocket);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task Send(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                CancellationToken.None);

            if (result != null && !result.CloseStatus.HasValue)
            {
                TweetHandler tweetHandler = new TweetHandler();
                while (true)
                {
                    string jsonTweet = JsonConvert.SerializeObject(tweetHandler.GetRandomTweet());
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonTweet)),
                        result.MessageType,
                        result.EndOfMessage,
                        System.Threading.CancellationToken.None);

                    //                Console.WriteLine(result);
                    //                _logger.LogInformation("Heeeey");
                    //                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                    //CancellationToken.None);
                    Thread.Sleep(2000);
                }
            }
            else
            {
                await webSocket.CloseAsync(result.CloseStatus.Value,
    result.CloseStatusDescription,
    System.Threading.CancellationToken.None);

                _logger.LogCritical("Client disconnected");
            }

        }
    }
}
