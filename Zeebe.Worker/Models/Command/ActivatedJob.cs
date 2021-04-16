namespace Zeebe.Worker.Models.Command
{
    public record ActivatedJob(
        long? Key,
        string Type,
        long WorkflowInstanceKey,
        string BpmnProcessId,
        int WorkflowDefinitionVersion,
        long WorkflowKey,
        string ElementId,
        long ElementInstanceKey,
        string CustomHeaders,
        string Worker,
        int Retries,
        long Deadline,
        string Variables);
}