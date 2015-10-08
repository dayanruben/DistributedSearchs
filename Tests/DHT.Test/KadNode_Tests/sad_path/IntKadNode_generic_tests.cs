// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using NUnit.Framework;

namespace DHT.Test.KadNode_Tests
{
    public partial class IntKadNode_generic_tests
    {
        [TestCase(5)]
        [ExpectedException(typeof (KeyNotFoundException))]
        public override void find_value_that_doesnt_exist_should_throw(int key)
        {
            base.find_value_that_doesnt_exist_should_throw(key);
        }
    }
}