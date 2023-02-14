using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Zeebe.Worker.Models;

namespace Zeebe.Worker.Controllers;

[ApiController]
[Route("[controller]")]
public class CalcController : ControllerBase
{
    private readonly ILogger<CalcController> _logger;

    public CalcController(ILogger<CalcController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// This action gets called from the Zeebe Dapr binding for each calc job that is executed inside
    /// the Zeebe process engine.
    /// </summary>
    [HttpPost("/calc")]
    public CalcResponse Calc([FromBody] CalcRequest request)
    {
        _logger.LogInformation("Zeebe JobKey: {Value}", Request.Headers["X-Zeebe-Job-Key"]);
        _logger.LogInformation("Zeebe JobType: {Value}", Request.Headers["X-Zeebe-Job-Type"]);
        _logger.LogInformation("Zeebe ProcessInstanceKey: {Value}", Request.Headers["X-Zeebe-Process-Instance-Key"]);
        _logger.LogInformation("Zeebe BpmnProcessId: {Value}", Request.Headers["X-Zeebe-Bpmn-Process-Id"]);
        _logger.LogInformation("Zeebe ProcessDefinitionVersion: {Value}", Request.Headers["X-Zeebe-Process-Definition-Version"]);
        _logger.LogInformation("Zeebe ProcessDefinitionKey: {Value}", Request.Headers["X-Zeebe-Process-Definition-Key"]);
        _logger.LogInformation("Zeebe ElementId: {Value}", Request.Headers["X-Zeebe-Element-Id"]);
        _logger.LogInformation("Zeebe ElementInstanceKey: {Value}", Request.Headers["X-Zeebe-Element-Instance-Key"]);
        _logger.LogInformation("Zeebe Worker: {Value}", Request.Headers["X-Zeebe-Worker"]);
        _logger.LogInformation("Zeebe Retries: {Value}", Request.Headers["X-Zeebe-Retries"]);
        _logger.LogInformation("Zeebe Deadline: {Value}", Request.Headers["X-Zeebe-Deadline"]);
        _logger.LogInformation("Custom header func: {Value}", Request.Headers["X-Custom-Header-Func"]);

        var (@operator, firstOperand, secondOperand) = request;
        var first = firstOperand.GetValueOrDefault();
        var second = secondOperand.GetValueOrDefault();
        return new CalcResponse(@operator switch
        {
            "+" => first + second,
            "-" => first - second,
            "/" => first / second,
            "*" => first * second,
            _ => throw new InvalidOperationException($"Operator {@operator} isn't supported")
        });
    }
}
