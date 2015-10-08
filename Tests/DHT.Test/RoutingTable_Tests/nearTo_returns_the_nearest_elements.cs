// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DHT.Test.RoutingTable_Tests
{
    [TestFixture]
    public class nearTo_returns_the_nearest_elements
    {
        [Test]
        public void returns_k_nearest_ordered()
        {
            RoutingTable<int> table = Mother.CreateIntRoutingTable();
            table.Add(new NodeIdentifier<int>("", 1));
            table.Add(new NodeIdentifier<int>("", 2));
            table.Add(new NodeIdentifier<int>("", 4));
            table.Add(new NodeIdentifier<int>("", 5));

            CollectionAssert.IsOrdered(table.NearTo(1).Select(n => n.NodeId),
                                       new LamdaComparer<int>((a, b) => a.CompareTo(b)));
        }

        [Test]
        public void returns_right_2_nearest()
        {
            RoutingTable<int> table = Mother.CreateIntRoutingTable();
            table.Add(new NodeIdentifier<int>("", 1));
            table.Add(new NodeIdentifier<int>("", 2));
            table.Add(new NodeIdentifier<int>("", 4));
            table.Add(new NodeIdentifier<int>("", 5));

            List<int> items = table.NearTo(2).Select(n => n.NodeId).ToList();
            Assert.AreEqual(items[0], 2);
            Assert.AreEqual(items[1], 1);
            Assert.AreEqual(items[2], 4);
            Assert.AreEqual(items[3], 5);
        }
    }
}