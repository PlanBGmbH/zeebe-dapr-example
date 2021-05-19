using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Toolkit.HighPerformance;

using Zeebe.Worker.Models;
using Zeebe.Worker.Models.Command;
using Zeebe.Worker.Utils;

namespace Zeebe.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public CommandController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet(Commands.Topology)]
        public async Task<TopologyResponse> Topology()
        {
            return await _daprClient.InvokeBindingAsync<object, TopologyResponse>("command", Commands.Topology, new {});
        }

        [HttpPost(Commands.DeployProcess)]
        public async Task<DeployProcessResponse> DeployProcess([FromForm] DeployProcessRequest request)
        {
            await using var memoryStream = new MemoryStream();
            var (fileContent, fileName) = request;
            await fileContent.OpenReadStream().CopyToAsync(memoryStream);
            var bindingRequest = new BindingRequest("command", Commands.DeployProcess)
            {
                Data = memoryStream.ToArray().AsMemory()
            };
            bindingRequest.Metadata.Add("fileName", fileName);

            var bindingResponse = await _daprClient.InvokeBindingAsync(bindingRequest);
            var responseJson = await JsonSerializer.DeserializeAsync<DeployProcessResponse>(
                bindingResponse.Data.AsStream(), SerializerOptions.Json);

            return responseJson;
        }

        [HttpPost(Commands.CreateInstance)]
        public async Task<ActionResult> CreateInstance([FromBody] CreateInstanceRequest request)
        {
            if (request.BpmnProcessId == null && request.ProcessDefinitionKey == null)
            {
                return BadRequest(new { error = "Either a bpmnProcessId or a processDefinitionKey must be given" });
            }

            var result = await _daprClient.InvokeBindingAsync<CreateInstanceRequest, CreateInstanceResponse>(
                "command", Commands.CreateInstance, request);

            return Ok(result);
        }

        [HttpPost(Commands.CancelInstance)]
        public async Task<NoContentResult> CancelInstance([FromBody] CancelInstanceRequest request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.CancelInstance, request);

            return NoContent();
        }

        [HttpPost(Commands.SetVariables)]
        public async Task<SetVariablesResponse> SetVariables([FromBody] SetVariablesRequest request)
        {
            return await _daprClient.InvokeBindingAsync<SetVariablesRequest, SetVariablesResponse>(
                "command", Commands.SetVariables, request);
        }

        [HttpPost(Commands.ResolveIncident)]
        public async Task<NoContentResult> ResolveIncident([FromBody] ResolveIncident request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.ResolveIncident, request);

            return NoContent();
        }

        [HttpPost(Commands.PublishMessage)]
        public async Task<PublishMessageResponse> PublishMessage([FromBody] PublishMessageRequest request)
        {
            return await _daprClient.InvokeBindingAsync<PublishMessageRequest, PublishMessageResponse>(
                "command", Commands.PublishMessage, request);
        }

        [HttpPost(Commands.ActivateJobs)]
        public async Task<IList<ActivatedJob>> PublishMessage([FromBody] ActivateJobsRequest request)
        {
            return await _daprClient.InvokeBindingAsync<ActivateJobsRequest, IList<ActivatedJob>>(
                "command", Commands.ActivateJobs, request);
        }

        [HttpPost(Commands.CompleteJob)]
        public async Task<NoContentResult> CompleteJob([FromBody] CompleteJobRequest request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.CompleteJob, request);

            return NoContent();
        }

        [HttpPost(Commands.FailJob)]
        public async Task<NoContentResult> FailJob([FromBody] FailJobRequest request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.FailJob, request);

            return NoContent();
        }

        [HttpPost(Commands.UpdateJobRetries)]
        public async Task<NoContentResult> UpdateJobRetries([FromBody] UpdateJobRetriesRequest request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.UpdateJobRetries, request);

            return NoContent();
        }

        [HttpPost(Commands.ThrowError)]
        public async Task<NoContentResult> ThrowError([FromBody] ThrowErrorRequest request)
        {
            await _daprClient.InvokeBindingAsync("command", Commands.ThrowError, request);

            return NoContent();
        }
    }
}