using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command
{
    public record UpdateJobRetriesRequest(
        [Required] long? JobKey,
        int? Retries);
}