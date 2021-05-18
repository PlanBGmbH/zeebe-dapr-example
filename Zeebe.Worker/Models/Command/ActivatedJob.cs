namespace Zeebe.Worker.Models.Command
{
    public record ActivatedJob(
        long? Key,
        string Type,
        long ProcessInstanceKey,
        string BpmnProcessId,
        int ProcessDefinitionVersion,
        long ProcessDefinitionKey,
        string ElementId,
        long ElementInstanceKey,
        string CustomHeaders,
        string Worker,
        int Retries,
        long Deadline,
        string Variables);
}