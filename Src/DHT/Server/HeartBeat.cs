// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Runtime.Serialization;

namespace DHT
{
    [DataContract]
    public class HeartBeat<TKey>
    {
        public HeartBeat(NodeIdentifier<TKey> nodeIdentifier)
        {
            NodeIdentifier = nodeIdentifier;
        }

        [DataMember]
        public NodeIdentifier<TKey> NodeIdentifier { get; private set; }
    }
}