// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using DHT.Test.KadNode_Tests;
using NUnit.Framework;

namespace DHT.Test.SetKadNode_Tests
{
    [TestFixture]
    [Category("Kademlia DHT")]
    public class IntSetKadNode_generic_tests : GenericSetKadNode_Tests<int, string, IntSetKadNode>
    {
        public override KadNetwork<int, IEnumerable<string>, IntSetKadNode> CreateKadNetwork()
        {
            var result = new IntSetKadNetwork();

            IntSetKadNode node1 = result.Add(2);
            IntSetKadNode node2 = result.Add(3);
            IntSetKadNode node3 = result.Add(4);
            IntSetKadNode node4 = result.Add(5);

            // all nodes knows about all others
            for (int i = 0; i < result.Count; i++)
                for (int j = 0; j < result.Count; j++)
                    if (j != i)
                        result[i].KadCore.RoutingTable.Add(result[j].KadCore.NodeIdentifier);

            return result;
        }

        [TestCase(1, new[] {"v1", "v2", "v3"})]
        public override void all_nodes_contain_the_same_value_after_store(int key, IEnumerable<string> value)
        {
            base.all_nodes_contain_the_same_value_after_store(key, value);
        }

        [TestCase(1, new[] {"v1", "v2", "v3"})]
        public override void find_value_returns_value_after_store(int key, IEnumerable<string> value)
        {
            base.find_value_returns_value_after_store(key, value);
        }

        [TestCase(1)]
        [ExpectedException(typeof (KeyNotFoundException))]
        public override void find_value_that_doesnt_exist_should_throw(int key)
        {
            base.find_value_that_doesnt_exist_should_throw(key);
        }

        [TestCase(1, new[] {"v1", "v2", "v3"})]
        public override void only_k_nodes_store_a_value_after_StoreLookUp(int key, IEnumerable<string> value)
        {
            base.only_k_nodes_store_a_value_after_StoreLookUp(key, value);
        }

        [TestCase(1, "v1")]
        public override void StoreInto_with_single_value(int key, string value)
        {
            base.StoreInto_with_single_value(key, value);
        }
    }
}