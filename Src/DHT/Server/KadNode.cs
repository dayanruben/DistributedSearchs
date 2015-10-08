// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using log4net;

namespace DHT
{
    /// <summary>
    ///   implementation of the kademelia service.
    ///   One of the constructors of the class <see cref = "ServiceHost" /> allows to host a service
    ///   given the class, this works for us because every node needs to be configured before (or 
    ///   after) it's hosted specifying for example the KnownNodes to join to.
    /// </summary>
    /// <typeparam name = "TKey"></typeparam>
    /// <typeparam name = "TValue"></typeparam>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class KadNode<TKey, TValue> : IKadNode<TKey, TValue>, IEquatable<KadNode<TKey, TValue>> where TValue : class
    {
        // ** todo try to separate the service calls from this class to make it testeable...
        // todo maybe all the node identifier parameters could be passed in the service metadata instead of as a parameter of each method call
        // question: The keys must be 160-bit long, or 135-bit also works (such as MD5)

        public DataStore<TKey, TValue> DataStore = new DataStore<TKey, TValue>();

        public KadNode(IKadCore<TKey, TValue> kadCore)
        {
            KadCore = kadCore;
        }

        #region Service Calls

        public virtual HeartBeat<TKey> Ping(NodeIdentifier<TKey> callerIdentifier = null)
        {
            Debug.Assert(KadCore.NodeIdentifier != null, "kadCore.NodeIdentifier != null");

            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            KadLogger.Info("Ping method called from " + callerIdentifier);
            if (callerIdentifier == null)
                KadLogger.Warn("Caller identifier is null.");
            KadLogger.Info("Returning NodeIdentifier: " + KadCore.NodeIdentifier);

            // This method could return valuable information about current/max performance, payload, etc.
            // also some data about the host: machine name, user, total files shared, etc.
            return new HeartBeat<TKey>(KadCore.NodeIdentifier);
        }

        /// <summary>
        ///   Store the pair (key, value)
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        /// <param name = "callerIdentifier"></param>
        public virtual void Store(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Debug.Assert(KadCore.NodeIdentifier != null, "kadCore.NodeIdentifier != null");

            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            if (DataStore.Contains(key))
                DataStore[key] = value;
            else DataStore.Add(key, value);

            KadLogger.Info(string.Format("Store method called with params ({0},{1}) from {2} ", key, value, callerIdentifier));
            if (callerIdentifier == null)
                KadLogger.Warn("Caller identifier is null.");
        }

        /// <summary>
        ///   return the K closest nodes to the key that this node knows about.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "callerIdentifier"></param>
        /// <returns></returns>
        public virtual FindNodeResult<TKey> FindNode(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Debug.Assert(KadCore.NodeIdentifier != null, "kadCore.NodeIdentifier != null");

            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            KadLogger.Info(string.Format("FindNode method called with key {0} from {1} ", key, callerIdentifier));
            if (callerIdentifier == null) KadLogger.Warn("Caller identifier is null.");

            // returns the K nearest elements that this node knows about around the key);
            return new FindNodeResult<TKey>(GetKClosestContacts(key));
        }

        /// <summary>
        ///   If the node has received a Store with that key, then return the value,
        ///   if not, return the K closest nodes to the key that this node knows about.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "callerIdentifier"></param>
        /// <returns></returns>
        public virtual FindValueResult<TKey, TValue> FindValue(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Debug.Assert(KadCore.NodeIdentifier != null, "kadCore.NodeIdentifier != null");

            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            KadLogger.Info(string.Format("FindValue method called with key {0} from {1} ", key, callerIdentifier));
            if (callerIdentifier == null) KadLogger.Warn("Caller identifier is null.");

            return DataStore.Contains(key)
                       ? new FindValueResult<TKey, TValue>(DataStore[key])
                       : new FindValueResult<TKey, TValue>(GetKClosestContacts(key));
        }

        public virtual bool Remove(TKey key, NodeIdentifier<TKey> callerIdentifier = null)
        {
            Debug.Assert(KadCore.NodeIdentifier != null, "kadCore.NodeIdentifier != null");

            // notify about the existence of the caller node
            if (callerIdentifier != null) OnNewNodeNotice(callerIdentifier);

            KadLogger.Info(string.Format("Remove method called with key {0} from {1} ", key, callerIdentifier));
            if (callerIdentifier == null) KadLogger.Warn("Caller identifier is null.");

            return DataStore.Remove(key);
        }

        /// <summary>
        ///   returns the k closest contacts to the given key
        /// </summary>
        /// <param name = "key"></param>
        /// <returns></returns>
        protected IEnumerable<NodeIdentifier<TKey>> GetKClosestContacts(TKey key)
        {
            return KadCore.RoutingTable.NearTo(key).Take(KadCore.Settings.K);
        }

        #endregion Service Calls

        public IKadCore<TKey, TValue> KadCore { get; private set; }

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
            KadCore.JoinToNetwork(knownNodesIdentifiers);
        }

        /// <summary>
        ///   Called every time that the Kademlia node notice a new node in the network. Can be 
        ///   either because the new node make a RPC request or was found in a node lookUp procedure.
        /// </summary>
        /// <param name = "nodeIdentifier">New node identifier.</param>
        protected virtual void OnNewNodeNotice(NodeIdentifier<TKey> nodeIdentifier)
        {
            KadCore.RegisterNewNode(nodeIdentifier);
            KadLogger.Info("New node noticed, adding it to the routing table. Node: " + nodeIdentifier);
        }

        public override string ToString()
        {
            return string.Format("NodeIdentifier: {0}", KadCore.NodeIdentifier);
        }

        #region Equality members

        public bool Equals(KadNode<TKey, TValue> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.KadCore.NodeIdentifier, KadCore.NodeIdentifier);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (KadNode<TKey, TValue>)) return false;
            return Equals((KadNode<TKey, TValue>) obj);
        }

        public override int GetHashCode()
        {
            return KadCore.NodeIdentifier.GetHashCode();
        }

        public static bool operator ==(KadNode<TKey, TValue> left, KadNode<TKey, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(KadNode<TKey, TValue> left, KadNode<TKey, TValue> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}