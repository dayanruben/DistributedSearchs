using System;

namespace DHT.Test.KadNode_Tests
{
    public class MockKadCore : KadCore<int,string>,IKadCore<int, string>
    {
        public MockKadCore(RoutingTable<int> routingTable, Func<NodeIdentifier<int>, IKadNode<int, string>> createClientFromNodeIdDelegate) : base(routingTable, createClientFromNodeIdDelegate)
        {
        }

        public MockKadCore(RoutingTable<int> routingTable, KademeliaSettings settings, Func<NodeIdentifier<int>, IKadNode<int, string>> createClientFromNodeIdDelegate, NodeIdentifier<int> nodeIdentifier) : base(routingTable, settings, createClientFromNodeIdDelegate, nodeIdentifier)
        {
        }

        protected override Type GetContractType
        {
            get { return typeof (int); }
        }
    }
}