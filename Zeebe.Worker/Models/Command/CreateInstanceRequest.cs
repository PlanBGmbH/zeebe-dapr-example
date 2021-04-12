using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command
{
    public record CreateInstanceRequest(
        string BpmnProcessId,
        long? WorkflowKey,
        short? Version,
        Dictionary<string, string> Variables);
}