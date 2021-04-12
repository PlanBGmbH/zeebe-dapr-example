namespace Zeebe.Worker.Models.Command
{
    public record CreateInstanceResponse(
        long? WorkflowKey,
        string BpmnProcessId,
        short? Version,
        long? WorkflowInstanceKey);
}