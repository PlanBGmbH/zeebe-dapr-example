using System.Collections.Generic;

namespace Zeebe.Worker.Models.Workflow
{
    public record DeployResponse(long Key, IList<DeployResponseWorkflow> Workflows);

    public record DeployResponseWorkflow(
        string BpmnProcessId,
        short Version,
        long WorkflowKey,
        string ResourceName);
}