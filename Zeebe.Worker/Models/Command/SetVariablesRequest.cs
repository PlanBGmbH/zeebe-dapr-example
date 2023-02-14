using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command;

public record SetVariablesRequest(
    [Required] long? ElementInstanceKey,
    [Required] object Variables,
    bool? Local);