// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DHT
{
    /// <summary>
    ///   this class manages the adition or removal of new known nodes to a kademelia node.
    ///   also find the nearest items to a specified index
    /// </summary>
    /// <remarks>
    ///   There are a lot of improvements that can be added to this class such as using a tree
    ///   to represent the data, or using a cache with the new nodes (see 13 pages specs)
    /// </remarks>
    public class RoutingTable<TKey> : ICollection<NodeIdentifier<TKey>>
    {
        private readonly List<NodeIdentifier<TKey>> _nodeIdentifiers = new List<NodeIdentifier<TKey>>();

        public RoutingTable(Metric<TKey> metric)
        {
            Metric = metric;
        }

        public Metric<TKey> Metric { get; set; }

        #region ICollection<NodeIdentifier<TKey>> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodeIdentifiers.GetEnumerator();
        }

        public int Count
        {
            get { return _nodeIdentifiers.Count; }
        }

        public IEnumerator<NodeIdentifier<TKey>> GetEnumerator()
        {
            return _nodeIdentifiers.GetEnumerator();
        }

        public void CopyTo(NodeIdentifier<TKey>[] array, int count)
        {
            _nodeIdentifiers.CopyTo(array, count);
        }

        public bool Contains(NodeIdentifier<TKey> item)
        {
            return _nodeIdentifiers.Contains(item);
        }

        public void Clear()
        {
            _nodeIdentifiers.Clear();
        }

        public void Add(NodeIdentifier<TKey> item)
        {
            Debug.Assert(item != null, "item != null");
            _nodeIdentifiers.Add(item);
        }

        public bool Remove(NodeIdentifier<TKey> item)
        {
            return _nodeIdentifiers.Remove(item);
        }

        bool ICollection<NodeIdentifier<TKey>>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        public virtual IEnumerable<NodeIdentifier<TKey>> NearTo(TKey key)
        {
            // if this method is implemented as a factory returning all closer items to the
            // given key while it builds them, then the closer k could be taken using LINQ

            return _nodeIdentifiers.OrderBy(nodeId => nodeId.NodeId,
                new LamdaComparer<TKey>((n1, n2) => DistanceComparer(n1, n2, key)));
        }

        public int DistanceComparer(TKey node1Key, TKey node2Key, TKey key)
        {
            return DistanceComparer(node1Key, node2Key, key, Metric);
        }

        public int DistanceComparer(NodeIdentifier<TKey> node1, NodeIdentifier<TKey> node2, TKey key)
        {
            return DistanceComparer(node1.NodeId, node2.NodeId, key);
        }

        /// <summary>
        ///   this function can be used as a comparer with the "closest to key" criteria
        /// </summary>
        /// <param name = "node1"></param>
        /// <param name = "node2"></param>
        /// <param name = "key"></param>
        /// <param name = "metric">metric used to calculate the distance</param>
        /// <returns></returns>
        public static int DistanceComparer(NodeIdentifier<TKey> node1, NodeIdentifier<TKey> node2, TKey key,
                                           Metric<TKey> metric)
        {
            return DistanceComparer(node1.NodeId, node2.NodeId, key, metric);
        }

        /// <summary>
        ///   this function can be used as a comparer with the "closest to key" criteria
        /// </summary>
        /// <param name = "node1"></param>
        /// <param name = "node2"></param>
        /// <param name = "key"></param>
        /// <param name = "metric">metric used to calculate the distance</param>
        /// <returns></returns>
        public static int DistanceComparer(TKey node1Key, TKey node2Key, TKey key, Metric<TKey> metric)
        {
            return metric.Distance(node1Key, key) - metric.Distance(node2Key, key);
        }

        public void AddRange(IEnumerable<NodeIdentifier<TKey>> collection)
        {
            _nodeIdentifiers.AddRange(collection);
        }
    }
}