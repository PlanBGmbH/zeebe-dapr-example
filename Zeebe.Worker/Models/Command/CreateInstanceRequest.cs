using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command;

public record CreateInstanceRequest(
    string BpmnProcessId,
    long? ProcessDefinitionKey,
    int? Version,
    Dictionary<string, string> Variables);