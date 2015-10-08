// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using DHT.Test.KadNode_Tests;
using NUnit.Framework;

namespace DHT.Test.SetKadNode_Tests
{
    public abstract class GenericSetKadNode_Tests<TKey, TValue, TKadNode> :
        GenericIKadNode_Tests<TKey, IEnumerable<TValue>, TKadNode>
        where TKadNode : SetKadNode<TKey, TValue> where TValue : class
    {
        public virtual void StoreInto_with_single_value(TKey key, TValue value)
        {
            KadNetwork<TKey, IEnumerable<TValue>, TKadNode> network = CreateKadNetwork();
            TKadNode node = network[0];

            node.StoreInto(key, value);

            FindValueResult<TKey, IEnumerable<TValue>> result = node.FindValue(key);

            CollectionAssert.AreEquivalent(new[] {value}, result.Value);
        }

        public override void all_nodes_contain_the_same_value_after_store(TKey key, IEnumerable<TValue> value)
        {
            KadNetwork<TKey, IEnumerable<TValue>, TKadNode> network = CreateKadNetwork();
            TKadNode node1 = network.First();

            node1.KadCore.StoreLookUp(key, value);

            IEnumerable<IEnumerable<TValue>> values = from node in network
                                                      let result = node.FindValue(key, node1.KadCore.NodeIdentifier)
                                                      where result.HasValue
                                                      select result.Value;

            foreach (var v in values)
                CollectionAssert.AreEquivalent(value, v);
        }
    }
}