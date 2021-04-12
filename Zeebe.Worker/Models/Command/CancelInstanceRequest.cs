using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Command
{
    public record CancelInstanceRequest([Required] long? WorkflowInstanceKey);
}