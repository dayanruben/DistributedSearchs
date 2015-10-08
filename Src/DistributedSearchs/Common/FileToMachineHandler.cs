// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ServiceModel;
using DistributedSearchs;

namespace DHT.MainModule
{
    public class FileToMachineHandler : KademeliaSetHandler<FileId, FileLocation, IFileToMachineServiceContract>,
                                        IPerformValueLookUp<FileId, IEnumerable<FileLocation>>
    {
        private IFileToMachineServiceContract _service;
        private FileToMachineCore _kadCore;

        public FileToMachineHandler(Metric<FileId> metric) : base(metric)
        {
        }

        public FileToMachineHandler(RoutingTable<FileId> routingTable) : base(routingTable)
        {
        }

        protected override ISetKadCore<FileId, FileLocation> KadCore
        {
            get { return _kadCore ?? (_kadCore = new FileToMachineCore(RoutingTable, CreateClientFromNodeId)); }
        }

        protected override IFileToMachineServiceContract Service
        {
            get { return _service ?? (_service = new FileToMachineServiceContract(KadCore)); }
        }

        protected override ISetKadNode<FileId, FileLocation> CreateClientFromNodeId(
            NodeIdentifier<FileId> nodeIdentifier)
        {
            return nodeIdentifier == NodeIdentifier
                       ? Service
                       : new FileToMachineClient(new BasicHttpBinding(), new EndpointAddress(nodeIdentifier.ServiceUrl));
        }
    }
}