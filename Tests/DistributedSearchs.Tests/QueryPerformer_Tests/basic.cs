// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace DistributedSearchs.Tests.QueryPerformer_Tests
{
    [TestFixture]
    public class basic
    {
        [Test]
        public void SimpleQuery()
        {
            QueryPerformer target = Mother.CreateQueryPerformer();

            target.Query("a");

            Assert.True(target.Results.Count() == 1);
            Assert.True(target.Results.First().Locations.Count() == 1);

            Assert.AreEqual("a1", target.Results.First().Locations.First().ServiceUrl);
        }

        [Test]
        public void two_words_with_one_file()
        {
            QueryPerformer target = Mother.CreateQueryPerformer();

            target.Query("a aa");

            Assert.True(target.Results.Count() == 1);
            Assert.True(target.Results.First().Locations.Count() == 1);

            //            CollectionAssert.AreEquivalent(new[]{"a1","aa1"},target.Results.First().Parts);
            Assert.AreEqual("a1", target.Results.First().Locations.First().ServiceUrl);
        }
    }
}