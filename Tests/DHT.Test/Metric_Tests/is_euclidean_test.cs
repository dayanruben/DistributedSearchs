// Developed by doiTTeam => devdoiTTeam@gmail.com

using NUnit.Framework;

namespace DHT.Test.XorMetric_Tests
{
    [TestFixture]
    [Ignore]
    public class is_euclidean_test : GenericMetricEuclideanTest<string, XorMetric>
    {
        protected override XorMetric CreateMetric()
        {
            return Mother.CreateXorMetric();
        }

        [TestCase("a")]
        public override void CheckSimetricProperty(string x)
        {
            base.CheckSimetricProperty(x);
        }

        [TestCase("a", "b", "c")]
        public override void CheckTriangleProperty(string x, string y, string z)
        {
            base.CheckTriangleProperty(x, y, z);
        }

        [TestCase("a", "b")]
        public override void CheckAsimetricProperty(string x, string y)
        {
            base.CheckAsimetricProperty(x, y);
        }

        [TestCase("a", "b")]
        public override void CheckSomethingProperty(string x, string y)
        {
            base.CheckSomethingProperty(x, y);
        }
    }
}