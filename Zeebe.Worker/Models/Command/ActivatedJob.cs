namespace Zeebe.Worker.Models.Command
{
    public record ActivatedJob(
        long? Key,
        string Type,
        long WorkflowInstanceKey,
        string BpmnProcessId,
        short WorkflowDefinitionVersion,
        long WorkflowKey,
        string ElementId,
        long ElementInstanceKey,
        string CustomHeaders,
        string Worker,
        short Retries,
        long Deadline,
        string Variables);
}