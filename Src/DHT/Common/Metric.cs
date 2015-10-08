// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DHT
{
    public abstract class Metric<T>
    {
        public abstract int Distance(T t1, T t2);
    }

    public class LambdaMetric<T> : Metric<T>
    {
        private readonly Func<T, T, int> _comparer;

        public LambdaMetric(Func<T, T, int> comparer)
        {
            _comparer = comparer;
        }

        public override int Distance(T t1, T t2)
        {
            return _comparer(t1, t2);
        }
    }

    public class XorMetric : Metric<string>
    {
        public override int Distance(string t1, string t2)
        {
            // todo implement the XOR metric based on the hash codes of these items
            return 0;
        }
    }
}