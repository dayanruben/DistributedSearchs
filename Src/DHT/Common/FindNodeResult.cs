// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DHT
{
    [DataContract(Name = "FindNodeResult{0}")]
    public class FindNodeResult<TKey>
    {
        public FindNodeResult(IEnumerable<NodeIdentifier<TKey>> nodes)
        {
            Nodes = nodes;
        }

        [DataMember]
        public IEnumerable<NodeIdentifier<TKey>> Nodes { get; private set; }
    }
}