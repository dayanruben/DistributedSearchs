// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace DHT
{
    public abstract class KademeliaSetHandler<TKey, TValue, TKadNode> where TValue : class
                                                                      where TKadNode : ISetKadNode<TKey, TValue>
    {
        protected RoutingTable<TKey> RoutingTable { get; private set; }

        public KademeliaSetHandler(Metric<TKey> metric)
            : this(new RoutingTable<TKey>(metric))
        {
        }

        public KademeliaSetHandler(RoutingTable<TKey> routingTable)
        {
            RoutingTable = routingTable;
//            KadCore = new SetKadCore<TKey, TValue>(routingTable, CreateClientFromNodeId);
        }

        /// <summary>
        ///   holds the current node Id. if the service is working, then this identifier is passed to
        ///   the <see cref = "KadCore" />
        /// </summary>
        public NodeIdentifier<TKey> NodeIdentifier { get; set; }

        protected abstract ISetKadCore<TKey, TValue> KadCore { get; }

        protected abstract TKadNode Service { get; }

        private ServiceHost ServiceHost { get; set; }

        public CommunicationState State
        {
            get { return ServiceHost.State; }
        }

        public void StartService()
        {
            if (ServiceHost == null)
                ServiceHost = new ServiceHost(Service);

            Debug.Assert(NodeIdentifier != null, "NodeIdentifier != null");
            KadCore.NodeIdentifier = NodeIdentifier;

            ServiceHost.Open();

            KadCore.JoinToNetwork();
        }

        public void CloseService()
        {
            KadCore.NodeIdentifier = null;

            ServiceHost.Close();
        }

        public IEnumerable<TValue> ValueLookUp(TKey key)
        {
            return KadCore.ValueLookUp(key);
        }

        public void StoreLookUp(TKey key, IEnumerable<TValue> value)
        {
            KadCore.StoreLookUp(key, value);
        }

        public void StoreIntoLookUp(TKey key, TValue value)
        {
            KadCore.StoreIntoLookUp(key, value);
        }

        public void StoreInto(TKey key, TValue value)
        {
            Service.StoreInto(key, value);
        }

        public void Store(TKey key, IEnumerable<TValue> value)
        {
            Service.Store(key, value);
        }

        protected abstract ISetKadNode<TKey, TValue> CreateClientFromNodeId(NodeIdentifier<TKey> nodeIdentifier);

        public void AddToRoutingTable(NodeIdentifier<TKey> nodeId)
        {
            KadCore.RoutingTable.Add(nodeId);
        }
    }
}