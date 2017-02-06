using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using NUnit.Framework;

namespace Tests.Model
{
    [TestFixture]
    public class DumpingPropertyTest
    {
        private DumpingProperty dumpingProperty;

        [SetUp]
        public void SetUp()
        {
            dumpingProperty = new DumpingProperty();
        }

        [Test]
        public void ConstructorWithNoParameters()
        {
            DumpingProperty dumpingProperty = new DumpingProperty();
            Assert.AreNotEqual(null, dumpingProperty);
        }

        [Test]
        [TestCase(Codes.ANALOG, 1)]
        [TestCase(Codes.DIGITAL, 1)]
        [TestCase(Codes.CONSUMER, 1)]
        [TestCase(Codes.CUSTOM, 1)]
        [TestCase(Codes.LIMITSET, 1)]
        [TestCase(Codes.MOTION, 1)]
        [TestCase(Codes.MULTIPLENODE, 1)]
        [TestCase(Codes.SENSOR, 1)]
        [TestCase(Codes.SINGLENODE, 1)]
        [TestCase(Codes.SOURCE, 1)]
        public void ConstructorWithGoodParameters(Codes code, double dumpingValue)
        {
            DumpingProperty dumpingProperty = new DumpingProperty(code, dumpingValue);
            Assert.AreEqual(dumpingValue, 1);
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(100, 1)]
        public void ConstructorWithBadParameters(Codes code, double dumpingValue)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                DumpingProperty dumpingProperty = new DumpingProperty(code, dumpingValue);
            });
        }


    }
}
