// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;

namespace DHT
{
    public interface ISetKadCore<TKey, TValue> : IKadCore<TKey, IEnumerable<TValue>>
    {
        void StoreIntoLookUp(TKey key, TValue value);
    }
}