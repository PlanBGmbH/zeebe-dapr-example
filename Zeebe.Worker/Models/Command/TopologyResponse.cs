using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command
{
    public record TopologyResponse(
        IList<BrokerInfo> Brokers,
        int? ClusterSize,
        int? PartitionsCount,
        int? ReplicationFactor,
        string GatewayVersion);

    public record BrokerInfo(
        int? NodeId,
        string Host,
        int? Port,
        IList<Partition> Partitions,
        string Version);

    public record Partition(
        int? PartitionId,
        int? Role,
        int? Health);
}