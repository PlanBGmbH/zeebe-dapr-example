using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Toolkit.HighPerformance;
using Zeebe.Worker.Models;
using Zeebe.Worker.Utils;

namespace Zeebe.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public WorkflowController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpPost("/deploy")]
        public async Task<DeployResponse> Deploy([FromForm] DeployRequest request)
        {
            using var memoryStream = new MemoryStream();
            await request.FileContent.OpenReadStream().CopyToAsync(memoryStream);
            var bindingRequest = new BindingRequest("workflow-deploy", "deploy")
            {
                Data = memoryStream.ToArray().AsMemory()
            };
            bindingRequest.Metadata.Add("fileName", request.FileName);
            bindingRequest.Metadata.Add("fileType", request.FileType);

            var bindingResponse = await _daprClient.InvokeBindingAsync(bindingRequest);
            var responseJson = await JsonSerializer.DeserializeAsync<DeployResponse>(
                bindingResponse.Data.AsStream(), SerializerOptions.Json);

            return responseJson;
        }

        [HttpPost("/create")]
        public async Task<CreateResponse> Create([FromBody] CreateRequest request)
        {
            return await _daprClient.InvokeBindingAsync<CreateRequest, CreateResponse>("workflow-create", "create", request);
        }
    }
}
