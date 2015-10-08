// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;

namespace DHT.Test.SetKadNode_Tests
{
    public class IntSetKadNode : SetKadNode<int, string>
    {
        private readonly IntSetKadNetwork _network;

        public IntSetKadNode(IKadCore<int, IEnumerable<string>> kadCore, IntSetKadNetwork network) : base(kadCore)
        {
            _network = network;
        }

        protected virtual IKadNode<int, IEnumerable<string>> CreateClientFromNodeId(NodeIdentifier<int> nodeIdentifier)
        {
            return _network.First(node => node.KadCore.NodeIdentifier == nodeIdentifier);
        }
    }
}