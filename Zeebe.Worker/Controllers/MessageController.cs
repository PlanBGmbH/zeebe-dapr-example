using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Zeebe.Worker.Models;

namespace Zeebe.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public MessageController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        /// <summary>
        /// Sends a message to the Zeebe workflow engine.
        /// </summary>
        [HttpPost("publish")]
        public async Task<PublishResponse> Publish([FromBody] PublishRequest request)
        {
            return await _daprClient.InvokeBindingAsync<PublishRequest, PublishResponse>("message", "publish", request);
        }
    }
}
