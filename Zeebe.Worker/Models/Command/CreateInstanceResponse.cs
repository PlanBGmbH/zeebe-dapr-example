namespace Zeebe.Worker.Models.Command
{
    public record CreateInstanceResponse(
        long? WorkflowKey,
        string BpmnProcessId,
        int? Version,
        long? WorkflowInstanceKey);
}