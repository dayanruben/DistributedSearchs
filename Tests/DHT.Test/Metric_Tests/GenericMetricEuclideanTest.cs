// Developed by doiTTeam => devdoiTTeam@gmail.com

using NUnit.Framework;

namespace DHT.Test.XorMetric_Tests
{
    public abstract class GenericMetricEuclideanTest<TKey, TMetric> where TMetric : Metric<TKey>
    {
        protected abstract TMetric CreateMetric();

        public virtual void CheckTriangleProperty(TKey x, TKey y, TKey z)
        {
            TMetric metric = CreateMetric();
            Assert.That(metric.Distance(x, y) + metric.Distance(y, z) >= metric.Distance(x, z));
        }

        public virtual void CheckSimetricProperty(TKey x)
        {
            TMetric metric = CreateMetric();
            Assert.That(metric.Distance(x, x) == 0);
        }

        public virtual void CheckSomethingProperty(TKey x, TKey y)
        {
            TMetric metric = CreateMetric();
            Assert.That(metric.Distance(x, y) > 0);
        }

        public virtual void CheckAsimetricProperty(TKey x, TKey y)
        {
            TMetric metric = CreateMetric();
            if (!x.Equals(y))
                Assert.That(metric.Distance(x, y) == metric.Distance(y, x));
        }
    }
}