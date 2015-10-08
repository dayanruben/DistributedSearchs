// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DHT.Test.KadNode_Tests
{
    public abstract class GenericIKadNode_Tests<TKey, TValue, TKadNode> where TKadNode : KadNode<TKey, TValue>
                                                                        where TValue : class
    {
        /// <summary>
        ///   this node should return a network with at less 2 nodes
        /// </summary>
        /// <returns></returns>
        public abstract KadNetwork<TKey, TValue, TKadNode> CreateKadNetwork();

        public virtual void all_nodes_contain_the_same_value_after_store(TKey key, TValue value)
        {
            KadNetwork<TKey, TValue, TKadNode> network = CreateKadNetwork();
            TKadNode node1 = network.First();

            node1.KadCore.StoreLookUp(key, value);

            IEnumerable<TValue> values = from node in network
                                         let result = node.FindValue(key, node1.KadCore.NodeIdentifier)
                                         where result.HasValue
                                         select result.Value;

            foreach (TValue v in values)
                Assert.AreEqual(value, v);
        }

        public virtual void find_value_returns_value_after_store(TKey key, TValue value)
        {
            KadNetwork<TKey, TValue, TKadNode> network = CreateKadNetwork();
            TKadNode node1 = network[0];
            TKadNode node2 = network[1];

            // store a value
            node1.Store(key, value, node2.KadCore.NodeIdentifier);
            // try to find it on the same node
            FindValueResult<TKey, TValue> result = node1.FindValue(key, node2.KadCore.NodeIdentifier);

            Assert.True(result.HasValue);
            Assert.AreEqual(value, result.Value);
        }

        public virtual void only_k_nodes_store_a_value_after_StoreLookUp(TKey key, TValue value)
        {
            KadNetwork<TKey, TValue, TKadNode> network = CreateKadNetwork();
            TKadNode node1 = network[0];

            node1.KadCore.StoreLookUp(key, value);

            IEnumerable<TValue> nodes = from node in network
                                        let result = node.FindValue(key, node1.KadCore.NodeIdentifier)
                                        where result.HasValue
                                        select result.Value;

            Assert.True(node1.KadCore.Settings.K >= nodes.Count());
            foreach (TValue v in nodes)
                Assert.AreEqual(value, v);
        }

        public virtual void find_value_that_doesnt_exist_should_throw(TKey key)
        {
            KadNetwork<TKey, TValue, TKadNode> network = CreateKadNetwork();
            TKadNode node = network.First();

            node.KadCore.ValueLookUp(key);
        }
    }
}