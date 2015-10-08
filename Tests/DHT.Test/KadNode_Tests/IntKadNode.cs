// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Linq;
using DHT.Test.KadNode_Tests;

namespace DHT.Test
{
    public class IntKadNode : KadNode<int, string>
    {
        private readonly KadNetwork<int, string, IntKadNode> _network;

        public IntKadNode(IKadCore<int, string> kadCore, KadNetwork<int, string, IntKadNode> network) : base(kadCore)
        {
            _network = network;
        }

        protected virtual IKadNode<int, string> CreateClientFromNodeId(NodeIdentifier<int> nodeIdentifier)
        {
            return _network.FirstOrDefault(node => node.KadCore.NodeIdentifier == nodeIdentifier);
        }
    }
}