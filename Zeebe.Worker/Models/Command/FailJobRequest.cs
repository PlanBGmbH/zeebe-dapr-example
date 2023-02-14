using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command;

public record FailJobRequest(
    [Required] long? JobKey,
    [Required] int? Retries,
    string ErrorMessage);