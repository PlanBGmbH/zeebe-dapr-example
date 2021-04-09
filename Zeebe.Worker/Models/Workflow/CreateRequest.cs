using System.Collections.Generic;

namespace Zeebe.Worker.Models.Workflow
{
    public record CreateRequest(
        string BpmnProcessId,
        long? WorkflowKey,
        short? Version,
        Dictionary<string, string> Variables);
}