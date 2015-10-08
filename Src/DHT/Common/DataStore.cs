// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;

namespace DHT
{
    public interface IDataStore<in TKey, TValue>
    {
        TValue this[TKey key] { get; }

        int Count { get; }
        bool Contains(TKey key);

        void Add(TKey key, TValue value);

        void Clear();
    }

    public class DataStore<TKey, TValue> : IDataStore<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dataStore = new Dictionary<TKey, TValue>();

        #region IDataStore<TKey,TValue> Members

        public TValue this[TKey key]
        {
            get { return _dataStore[key]; }
            set { _dataStore[key] = value; }
        }

        public int Count
        {
            get { return _dataStore.Count; }
        }

        public void Clear()
        {
            _dataStore.Clear();
        }

        public void Add(TKey key, TValue value)
        {
            _dataStore.Add(key, value);
        }

        public bool Contains(TKey key)
        {
            return _dataStore.ContainsKey(key);
        }

        #endregion

        public bool Remove(TKey key)
        {
            return _dataStore.Remove(key);
        }
    }
}