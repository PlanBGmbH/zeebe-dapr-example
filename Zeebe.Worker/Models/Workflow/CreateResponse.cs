namespace Zeebe.Worker.Models
{
    public class CreateResponse
    {
        public long? WorkflowKey { get; set; }

        public string BpmnProcessId { get; set; }

        public short? Version { get; set; }

        public long? WorkflowInstanceKey { get; set; }
    }
}
