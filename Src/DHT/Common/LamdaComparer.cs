// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace DHT
{
    public class LamdaComparer<T> : IComparer<T>, IComparer
    {
        private readonly Func<T, T, int> _comparer;

        public LamdaComparer(Func<T, T, int> comparer)
        {
            _comparer = comparer;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            return _comparer((T) x, (T) y);
        }

        #endregion

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return _comparer(x, y);
        }

        #endregion
    }
}