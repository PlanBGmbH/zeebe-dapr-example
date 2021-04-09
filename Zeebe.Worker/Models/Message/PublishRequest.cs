using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models.Message
{
    public record PublishRequest(
        [Required] string MessageName,
        string CorrelationKey,
        string MessageId,
        string TimeToLive,
        Dictionary<string, string> Variables);
}