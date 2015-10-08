// Developed by doiTTeam => devdoiTTeam@gmail.com

using System;

namespace DHT.Test
{
    public static class Mother
    {
        public static XorMetric CreateXorMetric()
        {
            return new XorMetric();
        }

        public static RoutingTable<int> CreateIntRoutingTable()
        {
            return new RoutingTable<int>(new LambdaMetric<int>((a, b) => Math.Abs(a - b)));
        }
    }
}