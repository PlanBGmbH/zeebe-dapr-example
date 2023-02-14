using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command;

public record ThrowErrorRequest(
    [Required] long? JobKey,
    [Required] string ErrorCode,
    string ErrorMessage);