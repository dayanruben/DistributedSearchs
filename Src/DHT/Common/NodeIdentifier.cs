// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Runtime.Serialization;

namespace DHT
{
    [DataContract(Name = "NodeIdentifier{0}")]
    public class NodeIdentifier<TKey> : IEquatable<NodeIdentifier<TKey>>
    {
        // this is the class stored in the k-buckets and is intended to replace
        // the udp protocol, as we don't need the (ip, port, nodeId) of each machine...

        // this class needs to provide a way for any client to connect to a node
        // probably with passing information to build an endpoint

        // to generate a random string key the following code may be used (in Python)
        // hash = hashlib.sha1()
        // hash.update(str(random.getrandbits(255)))
        // return hash.digest()

        public NodeIdentifier(string serviceUrl, TKey nodeId)
        {
            ServiceUrl = serviceUrl;
            NodeId = nodeId;
        }

        [DataMember(Name = "ServiceUrl")]
        public string ServiceUrl { get; private set; }

        [DataMember(Name = "NodeId")]
        public TKey NodeId { get; private set; }
        
        public override string ToString()
        {
            return string.Format("NodeId: {0}", NodeId);
        }

        #region Equality members

        public bool Equals(NodeIdentifier<TKey> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.NodeId, NodeId);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (NodeIdentifier<TKey>)) return false;
            return Equals((NodeIdentifier<TKey>) obj);
        }

        public override int GetHashCode()
        {
            return NodeId.GetHashCode();
        }

        public static bool operator ==(NodeIdentifier<TKey> left, NodeIdentifier<TKey> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NodeIdentifier<TKey> left, NodeIdentifier<TKey> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}