using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command
{
    public record DeployWorkflowResponse(long Key, IList<WorkflowMetadata> Workflows);

    public record WorkflowMetadata(
        string BpmnProcessId,
        short Version,
        long WorkflowKey,
        string ResourceName);
}