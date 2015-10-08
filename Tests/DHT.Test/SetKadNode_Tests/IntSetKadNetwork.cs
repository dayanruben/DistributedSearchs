// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using DHT.Test.KadNode_Tests;

namespace DHT.Test.SetKadNode_Tests
{
    public class IntSetKadNetwork : KadNetwork<int, IEnumerable<string>, IntSetKadNode>
    {
        public IntSetKadNode ExtraNode
        {
            get { return CreateNode(Count); }
        }

        public IntSetKadNode CreateNode(int nodeId)
        {
            RoutingTable<int> routingTable = Mother.CreateIntRoutingTable();
            var settings = new KademeliaSettings(TimeSpan.FromMilliseconds(100), 2, 5);

            return new IntSetKadNode(new MockSetKadCore(routingTable, settings, CreateClientFromNodeId,
                                                                 new NodeIdentifier<int>("", nodeId)), this);
        }

        public IntSetKadNode Add(int nodeid)
        {
            IntSetKadNode result = CreateNode(nodeid);
            Add(result);
            return result;
        }

        public ISetKadCore<int, string> GetParticipant()
        {
            return new MockSetKadCore(Mother.CreateIntRoutingTable(), CreateClientFromNodeId);
        }

        protected ISetKadNode<int, string> CreateClientFromNodeId(NodeIdentifier<int> nodeIdentifier)
        {
            return this.FirstOrDefault(node => node.KadCore.NodeIdentifier == nodeIdentifier);
        }
    }
}