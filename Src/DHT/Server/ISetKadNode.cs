// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.ServiceModel;

namespace DHT
{
    [ServiceContract]
    public interface ISetKadNode<TKey, TValue> : IKadNode<TKey, IEnumerable<TValue>> where TValue : class
    {
        [OperationContract]
        void StoreInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null);

        /// <summary>
        ///   As the values are a collection we may want to delete just the tuple
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        /// <param name = "callerIdentifier"></param>
        /// <returns></returns>
        [OperationContract]
        bool RemoveInto(TKey key, TValue value, NodeIdentifier<TKey> callerIdentifier = null);

        /// <summary>
        ///   Gets the elements associated with the given key, with pagination.
        /// </summary>
        /// <param name = "key">Key</param>
        /// <param name = "pageIndex">Page index.</param>
        /// <param name = "pageCount">Number of elements in each page.</param>
        /// <param name = "callerIdentifier"></param>
        /// <returns></returns>
        [OperationContract]
        FindValueResult<TKey, IEnumerable<TValue>> FindPagedValue(TKey key, int pageIndex, int pageCount,
                                                                  NodeIdentifier<TKey> callerIdentifier = null);
    }

    public class FindPagedValueResult<TKey, TValue> : FindValueResult<TKey, IEnumerable<TValue>>
    {
        public FindPagedValueResult(IEnumerable<NodeIdentifier<TKey>> nodes) : base(nodes)
        {
        }

        public FindPagedValueResult(TValue value) : base(new[] {value})
        {
        }
    }
}