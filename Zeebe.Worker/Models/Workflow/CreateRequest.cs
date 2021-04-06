using System.Collections.Generic;

namespace Zeebe.Worker.Models
{
    public class CreateRequest
    {
        public string BpmnProcessId { get; set; }

        public long? WorkflowKey { get; set; }

        public short? Version { get; set; }

        public Dictionary<string, string> Variables { get; set; }
    }
}
