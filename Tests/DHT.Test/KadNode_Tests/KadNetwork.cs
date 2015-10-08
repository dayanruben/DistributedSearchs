// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections;
using System.Collections.Generic;

namespace DHT.Test.KadNode_Tests
{
    public abstract class KadNetwork<TKey, TValue, TKadNode> : ICollection<TKadNode> where TValue : class
                                                                                     where TKadNode :
                                                                                         KadNode<TKey, TValue>
    {
        private readonly List<TKadNode> _nodes = new List<TKadNode>();

        public TKadNode this[int node]
        {
            get { return _nodes[node]; }
        }

        #region ICollection<TKadNode> Members

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int Count
        {
            get { return _nodes.Count; }
        }

        public bool Remove(TKadNode item)
        {
            return _nodes.Remove(item);
        }

        public IEnumerator<TKadNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public bool Contains(TKadNode item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(TKadNode[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        public void Add(TKadNode item)
        {
            _nodes.Add(item);
        }

        #endregion
    }
}