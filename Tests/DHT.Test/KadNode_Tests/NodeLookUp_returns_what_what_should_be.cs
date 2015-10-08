// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DHT.Test.KadNode_Tests
{
    [TestFixture]
    [Category("Kademlia DHT")]
    public class nodeLookUp_returns_what_what_should_be
    {
        public IntKadNetwork CreateIntKadNetwork()
        {
            var result = new IntKadNetwork();

            IntKadNode node2 = result.Add(2);
            IntKadNode node3 = result.Add(3);
            IntKadNode node4 = result.Add(4);

            // all nodes knows about all others
            for (int i = 0; i < result.Count; i++)
                for (int j = 0; j < result.Count; j++)
                    if (j != i)
                        result[i].KadCore.RoutingTable.Add(result[j].KadCore.NodeIdentifier);

            // add node 5 only connected with node 2
            IntKadNode node5 = result.Add(5);
            node3.KadCore.RoutingTable.Add(node5.KadCore.NodeIdentifier);
            node5.KadCore.RoutingTable.Add(node3.KadCore.NodeIdentifier);

            return result;
        }

        [TestCase(0, new[] {2, 3})]
        [TestCase(2, new[] {2, 3})]
        [TestCase(5, new[] {4, 5})]
        [TestCase(7, new[] {4, 5})]
        public void node_lookup_returns_all_nodes_in_network(int nodeKey, IEnumerable<int> expectedResult)
        {
            IntKadNetwork network = CreateIntKadNetwork();
            IntKadNode node1 = network[0];

            IEnumerable<int> result = node1.KadCore.NodeLookUp(nodeKey).Select(n => n.NodeId);
            CollectionAssert.AreEquivalent(expectedResult, result);
        }
    }
}