using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Toolkit.HighPerformance;

using Zeebe.Worker.Models.Workflow;
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

        [HttpPost("deploy")]
        public async Task<DeployResponse> Deploy([FromForm] DeployRequest request)
        {
            await using var memoryStream = new MemoryStream();
            var (fileContent, fileName, fileType) = request;
            await fileContent.OpenReadStream().CopyToAsync(memoryStream);
            var bindingRequest = new BindingRequest("workflow", "deploy")
            {
                Data = memoryStream.ToArray().AsMemory()
            };
            bindingRequest.Metadata.Add("fileName", fileName);
            if (fileType != null) bindingRequest.Metadata.Add("fileType", fileType);

            var bindingResponse = await _daprClient.InvokeBindingAsync(bindingRequest);
            var responseJson = await JsonSerializer.DeserializeAsync<DeployResponse>(
                bindingResponse.Data.AsStream(), SerializerOptions.Json);

            return responseJson;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateRequest request)
        {
            if (request.BpmnProcessId == null && request.WorkflowKey == null)
            {
                return BadRequest(new
                {
                    error = "Either a bpmnProcessId or a workflowKey must be given"
                });
            }

            var result = await _daprClient.InvokeBindingAsync<CreateRequest, CreateResponse>("workflow", "create", request);

            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<NoContentResult> Cancel([FromBody] CancelRequest request)
        {
            await _daprClient.InvokeBindingAsync("workflow", "cancel", request);

            return NoContent();
        }
    }
}