using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command
{
    public record CompleteJobRequest(
        [Required] long? JobKey,
        object Variables);
}