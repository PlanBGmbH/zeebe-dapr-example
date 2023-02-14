namespace Zeebe.Worker.Models.Command;

public record CreateInstanceResponse(
    long? ProcessDefinitionKey,
    string BpmnProcessId,
    int? Version,
    long? ProcessInstanceKey);