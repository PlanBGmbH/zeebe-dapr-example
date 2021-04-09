namespace Zeebe.Worker.Models.Workflow
{
    public record CreateResponse(
        long? WorkflowKey,
        string BpmnProcessId,
        short? Version,
        long? WorkflowInstanceKey);
}