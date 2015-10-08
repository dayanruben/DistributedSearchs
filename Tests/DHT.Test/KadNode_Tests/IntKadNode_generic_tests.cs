// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using NUnit.Framework;

namespace DHT.Test.KadNode_Tests
{
    [TestFixture]
    [Category("Kademlia DHT")]
    public partial class IntKadNode_generic_tests : GenericIKadNode_Tests<int, string, IntKadNode>
    {
        public override KadNetwork<int, string, IntKadNode> CreateKadNetwork()
        {
            var result = new IntKadNetwork();

            IntKadNode node1 = result.Add(2);
            IntKadNode node2 = result.Add(3);
            IntKadNode node3 = result.Add(4);
            IntKadNode node4 = result.Add(5);

            // all nodes knows about all others
            for (int i = 0; i < result.Count; i++)
                for (int j = 0; j < result.Count; j++)
                    if (j != i)
                        result[i].KadCore.RoutingTable.Add(result[j].KadCore.NodeIdentifier);

            return result;
        }

        public IntKadNetwork CreateIntKadNetwork()
        {
            return CreateKadNetwork() as IntKadNetwork;
        }

        [TestCase(1, "abc")]
        public override void all_nodes_contain_the_same_value_after_store(int key, string value)
        {
            base.all_nodes_contain_the_same_value_after_store(key, value);
        }

        [TestCase(2, "abc")]
        public override void find_value_returns_value_after_store(int key, string value)
        {
            base.find_value_returns_value_after_store(key, value);
        }

        [TestCase(2, "abc")]
        public virtual void find_value_after_joining_the_network(int key, String value)
        {
            IntKadNetwork network = CreateIntKadNetwork();
            IntKadNode node1 = network[0];
            IntKadNode node2 = network[1];

            IntKadNode extraNode = network.ExtraNode;

            // store a value
            node1.Store(key, value, node2.KadCore.NodeIdentifier);
            // join extra node to the network
            extraNode.JoinToNetwork(new[] {node1.KadCore.NodeIdentifier, node2.KadCore.NodeIdentifier});

            // the value should exist now
            string result = extraNode.KadCore.ValueLookUp(key);

            Assert.NotNull(result);
            Assert.AreEqual(value, result);
        }

        [TestCase(2, "abc")]
        public override void only_k_nodes_store_a_value_after_StoreLookUp(int key, string value)
        {
            base.only_k_nodes_store_a_value_after_StoreLookUp(key, value);
        }

        [Test]
        public void knows_other_nodes_after_joining_the_network()
        {
            IntKadNetwork network = CreateIntKadNetwork();
            IntKadNode node1 = network[0];
            IntKadNode node2 = network[1];

            IntKadNode extraNode = network.ExtraNode;

            // join extra node to the network
            extraNode.JoinToNetwork(new[] {node1.KadCore.NodeIdentifier, node2.KadCore.NodeIdentifier});

            CollectionAssert.Contains(extraNode.KadCore.RoutingTable, node1.KadCore.NodeIdentifier);
            CollectionAssert.Contains(extraNode.KadCore.RoutingTable, node2.KadCore.NodeIdentifier);
        }

        [Test]
        public void single_node_is_separated_from_the_network()
        {
            IntKadNode node = CreateIntKadNetwork().ExtraNode;

            // the routing table of a separated node should be empty
            // and only contain one node
            Assert.AreEqual(1, node.KadCore.RoutingTable.Count);
        }
    }
}