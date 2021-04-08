using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models
{
    public class CancelRequest
    {
        [Required]
        public long? WorkflowInstanceKey { get; set; }
    }
}
