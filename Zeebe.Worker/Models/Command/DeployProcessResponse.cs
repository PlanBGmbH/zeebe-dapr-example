using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command;

public record DeployProcessResponse(long Key, IList<ProcessMetadata> Processes);

public record ProcessMetadata(
    string BpmnProcessId,
    int Version,
    long ProcessDefinitionKey,
    string ResourceName);