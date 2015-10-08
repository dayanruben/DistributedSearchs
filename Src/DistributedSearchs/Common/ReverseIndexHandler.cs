// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ServiceModel;
using DistributedSearchs;

namespace DHT.MainModule
{
    public class ReverseIndexHandler : KademeliaSetHandler<string, FileId, ReverseIndexServiceContract>,
                                       IPerformValueLookUp<string, IEnumerable<FileId>>
    {
        private ReverseIndexServiceContract _service;
        private ReverseIndexCore _kadCore;

        public ReverseIndexHandler(Metric<string> metric) : base(metric)
        {
        }

        public ReverseIndexHandler(RoutingTable<string> routingTable) : base(routingTable)
        {
        }

        protected override ISetKadCore<string, FileId> KadCore
        {
            get { return _kadCore ?? (_kadCore = new ReverseIndexCore(RoutingTable, CreateClientFromNodeId)); }
        }

        protected override ReverseIndexServiceContract Service
        {
            get { return _service ?? (_service = new ReverseIndexServiceContract(KadCore)); }
        }

        protected override ISetKadNode<string, FileId> CreateClientFromNodeId(NodeIdentifier<string> nodeIdentifier)
        {
            return nodeIdentifier == NodeIdentifier
                       ? Service
                       : (ISetKadNode<string, FileId>)
                         new ReverseIndexServiceClient(new BasicHttpBinding(),
                                                       new EndpointAddress(nodeIdentifier.ServiceUrl));
        }
    }
}