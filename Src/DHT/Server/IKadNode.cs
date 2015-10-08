// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.ServiceModel;

namespace DHT
{
    /// <summary>
    ///   Base interface for a Kademelia node, this should be the contract of the service.
    /// </summary>
    /// <remarks>
    ///   - all this calls affect only to the current node, they don't make any service request
    ///   to other nodes, see docs
    ///   - all method calls should store the <see cref = "NodeIdentifier" /> parameter in
    ///   the corresponding k-bucket
    /// </remarks>
    /// <typeparam name = "TKey"></typeparam>
    /// <typeparam name = "TValue"></typeparam>
    [ServiceContract(Namespace = "DHT")]
    public interface IKadNode<TKey, TValue> where TValue : class
    {
        [OperationContract]
        HeartBeat<TKey> Ping(NodeIdentifier<TKey> callerIdentifier = null);

        [OperationContract]
        void Store(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null);

        [OperationContract]
        FindNodeResult<TKey> FindNode(TKey key, NodeIdentifier<TKey> callerIdentifier = null);

        [OperationContract]
        FindValueResult<TKey, TValue> FindValue(TKey key, NodeIdentifier<TKey> callerIdentifier = null);

        [OperationContract]
        bool Remove(TKey key, NodeIdentifier<TKey> callerIdentifier = null);
    }
}