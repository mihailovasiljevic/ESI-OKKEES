using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Common;

namespace Tests.Model
{
    [TestFixture]
    public class DataSetTest
    {
        private DataSet dataSet = null;

        [SetUp]
        public void SetUp()
        {
            dataSet = new DataSet();
            dataSet.AddPair(Codes.DIGITAL, 1);
        }

        [Test]
        public void ConstructorWithNoParameters()
        {
            DataSet dataSet = new DataSet();
            Assert.AreNotEqual(null, dataSet);
            Assert.AreNotEqual(null, dataSet.GetDataSet);
        }

        [Test]
        [TestCase(Codes.ANALOG, 1)]
        [TestCase(Codes.DIGITAL, 1)]
        public void AddPairGoodParameters(Codes code, int value)
        {
            DataSet ds = new DataSet();
            ds.AddPair(code, value);
            Assert.IsNotEmpty(ds.GetDataSet);
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(100, 1)]
        [TestCase(Codes.ANALOG, 2)]
        public void AddPairBadParameters(Codes code, int value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dataSet.AddPair(code, value);
            });
        }

        [Test]
        public void ExistsInDataSetTrueParameters()
        {
            Assert.IsTrue(dataSet.ExistsInDataSet(Codes.DIGITAL));
        }

        [Test]
        [TestCase(Codes.CUSTOM)]
        [TestCase(Codes.SOURCE)]
        public void ExistsInDataSetFlaseParameters(Codes code)
        {
            Assert.IsFalse(dataSet.ExistsInDataSet(code));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void ExistsInDataSetBadParameters(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dataSet.ExistsInDataSet(code);
            });
        }

        [Test]
        public void SetDataSet()
        {
            dataSet.SetDataSet = new Dictionary<Codes, int>();
            dataSet.AddPair(Codes.CUSTOM, 1);
            Assert.AreEqual(1, dataSet.GetDataSet.Count);
        }

    }
}
