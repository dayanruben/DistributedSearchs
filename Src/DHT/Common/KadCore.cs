// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text.RegularExpressions;
using ReactiveUI;

namespace DHT
{
    public abstract class KadCore<TKey, TValue> : IKadCore<TKey, TValue> where TValue : class
    {
        private readonly Func<NodeIdentifier<TKey>, IKadNode<TKey, TValue>> _createClientFromNodeIdDelegate;
        private NodeIdentifier<TKey> _nodeIdentifier;

        /// <summary>
        ///   Initializes a new instance of <see cref = "kadCore{TKey,TValue}" /> with the 
        ///   default settings.
        /// </summary>
        /// <param name = "routingTable">routing table with the known node addresses</param>
        public KadCore(RoutingTable<TKey> routingTable,
                       Func<NodeIdentifier<TKey>, IKadNode<TKey, TValue>> createClientFromNodeIdDelegate)
            : this(routingTable, KademeliaSettings.Default, createClientFromNodeIdDelegate)
        {
        }

        public KadCore(RoutingTable<TKey> routingTable, KademeliaSettings settings,
                       Func<NodeIdentifier<TKey>, IKadNode<TKey, TValue>> createClientFromNodeIdDelegate,
                       NodeIdentifier<TKey> nodeIdentifier = null)
        {
            _createClientFromNodeIdDelegate = createClientFromNodeIdDelegate;
            RoutingTable = routingTable;
            Settings = settings;
            NodeIdentifier = nodeIdentifier;
        }

        #region IKadCore<TKey,TValue> Members

        public KademeliaSettings Settings { get; private set; }

        /// <summary>
        ///   the routing table should hold the metric
        /// </summary>
        public RoutingTable<TKey> RoutingTable { get; private set; }

        /// <summary>
        ///   identifier of the current node. the hash should be of 160 bits and must 
        ///   be randomly generated. The address may be a constructor parameter
        /// </summary>
        public NodeIdentifier<TKey> NodeIdentifier
        {
            get { return _nodeIdentifier; }
            set
            {
                // the current node id must be in the routing table
                if (_nodeIdentifier != null) RoutingTable.Remove(_nodeIdentifier);
                _nodeIdentifier = value;
                if (value != null) RoutingTable.Add(value);
            }
        }

        /// <summary>
        ///   this node joins to the network, process:
        ///   1. add know nodes to this nodes k-bucket
        ///   2. perform a NodeLookUp
        /// </summary>
        /// <remarks>
        ///   After FindNode is called the <paramref name = "knownNodesIdentifiers" /> knows about
        ///   the existence of this node...
        /// </remarks>
        /// <param name = "knownNodesIdentifiers"></param>
        public void JoinToNetwork(IEnumerable<NodeIdentifier<TKey>> knownNodesIdentifiers)
        {
            // 1. add known items to the routing table
            foreach (var nodeIdentifier in knownNodesIdentifiers)
                RoutingTable.Add(nodeIdentifier);

            // 2. Perform a Node Look up on the own node id
            JoinToNetwork();
        }

        /// <summary>
        ///   Locates the K closest nodes to the given ID (This is the most important procedure
        ///   that a Kademlia node must do).
        /// </summary>
        /// <remarks>
        ///   - See Section 2.3 Kademlia Protocol for a full break down
        /// </remarks>
        /// <param name = "key"></param>
        /// <returns></returns>
        public IEnumerable<NodeIdentifier<TKey>> NodeLookUp(TKey key)
        {
            // bug nodes should be ordered by it's distance to the given key
            return NodeLookUp(key, GetKClosestContacts(key));
        }

        /// <summary>
        ///   This method returns the value associated with the given key.
        /// </summary>
        /// <remarks>
        ///   It's similar to the <see cref = "NodeLookUp" /> method but using <see cref = "FindValue" />.
        /// </remarks>
        /// <param name = "key"></param>
        /// <returns></returns>
        public TValue ValueLookUp(TKey key)
        {
            //            IEnumerable<NodeIdentifier<TKey>> closestNodes = NodeLookUp(key).ToList();

            foreach (var nodeIdentifier in NodeLookUp(key))
            {
                try
                {
                    IKadNode<TKey, TValue> client = CreateClientFromNodeId(nodeIdentifier);
                    FindValueResult<TKey, TValue> findValueResult = client.FindValue(key, NodeIdentifier);
                    if (findValueResult.HasValue)
                        return findValueResult.Value;
                }
                catch (CommunicationException e)
                {
                    // the service isn't working properly or doesn't contain the given key
                }
            }
            throw new KeyNotFoundException("The key doesn't exist in the network.");
        }

        /// <summary>
        ///   Find the closest k closest nodes to the given key (using <see cref = "NodeLookUp" />) and stores
        ///   the (<paramref name = "key" />,<paramref name = "value" />) pair in those nodes.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        public void StoreLookUp(TKey key, TValue value)
        {
            // the calls to Store in the nodes can be asynchronous
            NodeLookUp(key).ToObservable()
                .Subscribe(node =>
                    {
                        try
                        {
                            IKadNode<TKey, TValue> client = CreateClientFromNodeId(node);
                            client.Store(key, value, NodeIdentifier);
                        }
                        catch (CommunicationException e)
                        {
                        }
                    });
        }

        /// <summary>
        ///   Called every time that the Kademlia node notice a new node in the network. Can be 
        ///   either because the new node make a RPC request or was found in a node lookUp procedure.
        /// </summary>
        /// <param name = "nodeIdentifier">New node identifier.</param>
        public virtual void RegisterNewNode(NodeIdentifier<TKey> nodeIdentifier)
        {
            if (!RoutingTable.Contains(nodeIdentifier))
                RoutingTable.Add(nodeIdentifier);
        }

        public void JoinToNetwork()
        {
            if (NodeIdentifier != null)
                NodeLookUp(NodeIdentifier.NodeId);
        }

        #endregion

        /// <summary>
        ///   returns the k closest contacts to the given key
        /// </summary>
        /// <param name = "key"></param>
        /// <returns></returns>
        protected IEnumerable<NodeIdentifier<TKey>> GetKClosestContacts(TKey key)
        {
            return RoutingTable.NearTo(key).Where(IsOnline).Take(Settings.K);
        }

        /// <summary>
        /// Returns true if the given node identifier is online
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsOnline(NodeIdentifier<TKey> node)
        {
//            Regex b = new Regex(@"http://\w+(\.\w+)*:\d+");
//            string baseAddress = b.Match(node.ServiceUrl).Value;
//            var mexAddress =new Uri (baseAddress + "/?WSDL");
//            return MetadataResolver.Resolve(GetContractType, mexAddress, MetadataExchangeClientMode.HttpGet).Count>0;
            
            // todo checking if a service is online is really expensive, find a better way to do this
            return true;
        }

        protected abstract Type GetContractType { get; }

        private IEnumerable<NodeIdentifier<TKey>> NodeLookUp(TKey key,
                                                             IEnumerable<NodeIdentifier<TKey>> knownClosestNodes)
        {
            // convert the closest nodes to a list to prevent multiple enumerations
            List<NodeIdentifier<TKey>> closestNodes = knownClosestNodes.ToList();

            List<NodeIdentifier<TKey>> orderedNodes = closestNodes.ToObservable()
                .CachedSelectMany(nodeId =>
                    {
                        IKadNode<TKey, TValue> client = CreateClientFromNodeId(nodeId);
                        FindNodeResult<TKey> nodes = client.FindNode(key, NodeIdentifier);
                        return Observable.Return(nodes);
                    }, maxConcurrent: Settings.Cita)
                .SelectMany(findNodeResult => findNodeResult.Nodes)
                .Merge(closestNodes.ToObservable())
                .Distinct()
                .Do(RegisterNewNode)
                .Timeout(Settings.ClientTimeOut)
                .OnErrorResumeNext(Observable.Empty<NodeIdentifier<TKey>>())
                .ToEnumerable()
                .OrderBy(nodeId => nodeId.NodeId,
                         new LamdaComparer<TKey>((n1, n2) => RoutingTable.DistanceComparer(n1, n2, key)))
                .Take(Settings.K)
                .ToList();

            return orderedNodes.SequenceEqual(closestNodes)
                       ? orderedNodes
                       : NodeLookUp(key, orderedNodes);
        }

        protected IKadNode<TKey, TValue> CreateClientFromNodeId(NodeIdentifier<TKey> node)
        {
            return _createClientFromNodeIdDelegate(node);
        }
    }
}