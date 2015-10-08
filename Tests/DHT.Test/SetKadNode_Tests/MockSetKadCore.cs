using System;
using System.Collections.Generic;

namespace DHT.Test.SetKadNode_Tests
{
    public class MockSetKadCore : SetKadCore<int,string>,IKadCore<int, IEnumerable<string>>
    {
        public MockSetKadCore(RoutingTable<int> routingTable, Func<NodeIdentifier<int>, ISetKadNode<int, string>> createClientFromNodeIdDelegate) : base(routingTable, createClientFromNodeIdDelegate)
        {
        }

        public MockSetKadCore(RoutingTable<int> routingTable, KademeliaSettings settings, Func<NodeIdentifier<int>, ISetKadNode<int, string>> createClientFromNodeIdDelegate, NodeIdentifier<int> nodeIdentifier) : base(routingTable, settings, createClientFromNodeIdDelegate, nodeIdentifier)
        {
        }

        protected override Type GetContractType
        {
            get { return typeof (int); }
        }
    }
}