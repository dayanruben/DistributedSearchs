// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Linq;

namespace DHT.Test.KadNode_Tests
{
    public class IntKadNetwork : KadNetwork<int, string, IntKadNode>
    {
        public IntKadNode ExtraNode
        {
            get { return CreateNode(Count); }
        }

        public IntKadNode CreateNode(int nodeId)
        {
            RoutingTable<int> routingTable = Mother.CreateIntRoutingTable();
            var settings = new KademeliaSettings(TimeSpan.FromMilliseconds(100), 2, 5);

            return
                new IntKadNode(
                    new MockKadCore(routingTable, settings, CreateClientFromNodeId,
                                             new NodeIdentifier<int>("", nodeId)), this);
        }

        public IntKadNode Add(int nodeid)
        {
            IntKadNode result = CreateNode(nodeid);
            Add(result);
            return result;
        }

        public IKadCore<int, string> GetParticipant()
        {
            return new MockKadCore(Mother.CreateIntRoutingTable(), CreateClientFromNodeId);
        }

        protected IKadNode<int, string> CreateClientFromNodeId(NodeIdentifier<int> nodeIdentifier)
        {
            return this.FirstOrDefault(node => node.KadCore.NodeIdentifier == nodeIdentifier);
        }
    }
}