// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DHT
{
    /// <summary>
    ///   Represent thew result of a <see cref = "IKadNode{TKey,TValue}.FindValue" /> method call.
    ///   This method should return the value in case that the node holds the key locally
    ///   (see <see cref = "Value" />), or a list of the k closest nodes to the given key (see
    ///   <see cref = "Nodes" />)
    /// </summary>
    /// <typeparam name = "TValue"></typeparam>
    [DataContract]
    public class FindValueResult<TKey, TValue> : FindNodeResult<TKey> where TValue : class
    {
        private FindValueResult(IEnumerable<NodeIdentifier<TKey>> nodes, TValue value)
            : base(nodes)
        {
            Value = value;
        }

        public FindValueResult(IEnumerable<NodeIdentifier<TKey>> nodes)
            : this(nodes, null)
        {
        }

        public FindValueResult(TValue value)
            : this(Enumerable.Empty<NodeIdentifier<TKey>>(), value)
        {
        }

        [DataMember]
        public TValue Value { get; set; }

        public bool HasValue
        {
            get { return Value != null; }
        }
    }
}