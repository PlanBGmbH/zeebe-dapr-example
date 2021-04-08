using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models
{
    public class PublishRequest
    {
        [Required]
        public string MessageName { get; set; }

        public string CorrelationKey { get; set; }

        public string MessageId { get; set; }

        public string TimeToLive { get; set; }

        public Dictionary<string, string> Variables { get; set; }
    }
}
