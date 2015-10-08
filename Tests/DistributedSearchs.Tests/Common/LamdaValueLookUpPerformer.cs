// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DistributedSearchs.Tests.Common
{
    public class LamdaValueLookUpPerformer<TKey, TValue> : IPerformValueLookUp<TKey, TValue>
    {
        private readonly Func<TKey, TValue> _valueLookUp;

        public LamdaValueLookUpPerformer(Func<TKey, TValue> valueLookUp)
        {
            _valueLookUp = valueLookUp;
        }

        #region IPerformValueLookUp<TKey,TValue> Members

        public TValue ValueLookUp(TKey key)
        {
            return _valueLookUp(key);
        }

        #endregion
    }
}