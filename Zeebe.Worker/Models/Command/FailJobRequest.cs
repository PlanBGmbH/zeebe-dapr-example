using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command
{
    public record FailJobRequest(
        [Required] long? JobKey,
        [Required] short? Retries,
        string ErrorMessage);
}