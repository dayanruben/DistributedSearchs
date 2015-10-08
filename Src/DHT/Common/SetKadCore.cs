// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DHT
{
    public abstract class SetKadCore<TKey, TValue> : KadCore<TKey, IEnumerable<TValue>>, ISetKadCore<TKey, TValue>
        where TValue : class
    {
        public SetKadCore(RoutingTable<TKey> routingTable,
                          Func<NodeIdentifier<TKey>, ISetKadNode<TKey, TValue>> createClientFromNodeIdDelegate)
            : base(routingTable, createClientFromNodeIdDelegate)
        {
        }

        public SetKadCore(RoutingTable<TKey> routingTable, KademeliaSettings settings,
                          Func<NodeIdentifier<TKey>, ISetKadNode<TKey, TValue>> createClientFromNodeIdDelegate,
                          NodeIdentifier<TKey> nodeIdentifier)
            : base(routingTable, settings, createClientFromNodeIdDelegate, nodeIdentifier)
        {
        }

        #region ISetKadCore<TKey,TValue> Members

        public void StoreIntoLookUp(TKey key, TValue value)
        {
            // 1. find the closest nodes to the given key
            IEnumerable<NodeIdentifier<TKey>> closestNodes = NodeLookUp(key);

            // 2. store the (key,value) pair in those nodes
            foreach (var node in closestNodes)
            {
                try
                {
                    ISetKadNode<TKey, TValue> client = CreateSetClientFromNodeId(node);
                    client.StoreInto(key, value, NodeIdentifier);
                }
                catch (EndpointNotFoundException e)
                {
                    KadLogger.Error("No endpoint found, probably because the node is offline.",e);
                }
            }
        }

        #endregion

        private ISetKadNode<TKey, TValue> CreateSetClientFromNodeId(NodeIdentifier<TKey> node)
        {
            return (ISetKadNode<TKey, TValue>) CreateClientFromNodeId(node);
        }
    }
}