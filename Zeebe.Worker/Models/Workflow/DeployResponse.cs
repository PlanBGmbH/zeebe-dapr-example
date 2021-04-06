using System.Collections.Generic;

namespace Zeebe.Worker.Models
{
    public class DeployResponse
    {
        public long Key { get; set; }

        public IList<DeploResponseWorkflow> Workflows { get; set; }
    }

    public class DeploResponseWorkflow
    {
        public string BpmnProcessId { get; set; }

        public short Version { get; set; }

        public long WorkflowKey { get; set; }

        public string ResourceName { get; set; }
    }
}
