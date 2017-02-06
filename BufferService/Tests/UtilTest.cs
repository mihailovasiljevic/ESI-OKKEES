using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        [TestCase(10, 10, 2001, true)]
        [TestCase(11, 11, 2001, true)]
        [TestCase(29, 2, 2008, true)]
        [TestCase(31, 2, 2001, false)]
        public void CheckDate(int day, int month, int year, bool assert)
        {
            Assert.AreEqual(Util.CheckDate(day, month, year), assert);
        }

        [Test]
        [TestCase(typeof(Codes), Codes.ANALOG, true)]
        [TestCase(typeof(Codes), Codes.DIGITAL, true)]
        public void ElementExistsInEnumeration(Type type, object obj, bool assert)
        {
            Assert.AreEqual(Util.ElementExistsInEnumeration(type, obj), assert);

        }
    }
}
