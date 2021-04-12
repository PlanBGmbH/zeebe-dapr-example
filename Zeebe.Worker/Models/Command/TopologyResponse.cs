using System.Collections.Generic;

namespace Zeebe.Worker.Models.Command
{
    public record TopologyResponse(
        IList<BrokerInfo> Brokers,
        short? ClusterSize,
        short? PartitionsCount,
        short? ReplicationFactor,
        string GatewayVersion);

    public record BrokerInfo(
        short? NodeId,
        string Host,
        short? Port,
        IList<Partition> Partitions,
        string Version);

    public record Partition(
        short? PartitionId,
        short? Role,
        short? Health);
}