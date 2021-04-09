using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Workflow
{
    public record CancelRequest([Required] long? WorkflowInstanceKey);
}