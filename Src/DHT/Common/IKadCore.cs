// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;

namespace DHT
{
    public interface IKadCore<TKey, TValue>
    {
        /// <summary>
        ///   the routing table should hold the metric
        /// </summary>
        RoutingTable<TKey> RoutingTable { get; }

        /// <summary>
        ///   identifier of the current node. the hash should be of 160 bits and must 
        ///   be randomly generated. The address may be a constructor parameter
        /// </summary>
        NodeIdentifier<TKey> NodeIdentifier { get; set; }

        KademeliaSettings Settings { get; }

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
        void JoinToNetwork(IEnumerable<NodeIdentifier<TKey>> knownNodesIdentifiers);

        /// <summary>
        ///   Locates the K closest nodes to the given ID (This is the most important procedure
        ///   that a Kademlia node must do).
        /// </summary>
        /// <remarks>
        ///   - See Section 2.3 Kademlia Protocol for a full break down
        /// </remarks>
        /// <param name = "key"></param>
        /// <returns></returns>
        IEnumerable<NodeIdentifier<TKey>> NodeLookUp(TKey key);

        /// <summary>
        ///   This method returns the value associated with the given key.
        /// </summary>
        /// <remarks>
        ///   It's similar to the <see cref = "NodeLookUp" /> method but 
        ///   using <see cref = "FindValue" />.
        /// </remarks>
        /// <param name = "key"></param>
        /// <returns></returns>
        TValue ValueLookUp(TKey key);

        /// <summary>
        ///   Find the closest k closest nodes to the given key (using <see cref = "NodeLookUp" />) and stores
        ///   the (<paramref name = "key" />,<paramref name = "value" />) pair in those nodes.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        void StoreLookUp(TKey key, TValue value);

        /// <summary>
        ///   Called every time that the Kademlia node notice a new node in the network. Can be 
        ///   either because the new node make a RPC request or was found in a node lookUp procedure.
        /// </summary>
        /// <param name = "nodeIdentifier">New node identifier.</param>
        void RegisterNewNode(NodeIdentifier<TKey> nodeIdentifier);

        void JoinToNetwork();
    }
}