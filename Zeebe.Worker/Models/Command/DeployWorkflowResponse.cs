using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command
{
    public record DeployWorkflowResponse(long Key, IList<WorkflowMetadata> Workflows);

    public record WorkflowMetadata(
        string BpmnProcessId,
        int Version,
        long WorkflowKey,
        string ResourceName);
}