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
    public class DataSetsTest
    {
        private DataSets dataSets = null;

        [SetUp]
        public void SetUp()
        {
            dataSets = new DataSets();
        }

        [Test]
        public void ConstructorWithoutParameters()
        {
            DataSets dataSets = new DataSets();

            Assert.AreNotEqual(null, dataSets);
            Assert.AreEqual(1, dataSets.GetDataSet(0).GetDataSet.ElementAt(0).Value);
        }

        [Test]
        [TestCase(Codes.ANALOG)]
        public void GetDataSetByCodeGoodParameters(Codes code)
        {
            DataSet dataSet = dataSets.GetDataSet(code);

            Assert.AreEqual(Codes.ANALOG, dataSet.GetDataSet.ElementAt(0).Key);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void GetDataSetByCodeBadParameters(Codes code)
        {
            
            Assert.Throws<ArgumentException>(() =>
            {
                DataSet dataSet = dataSets.GetDataSet(code);
            });
        }

        [Test]
        [TestCase(0)]
        public void GetDataSetByIndexGoodParameters(int index)
        {
            DataSet dataSet = dataSets.GetDataSet(index);

            Assert.AreEqual(Codes.ANALOG, dataSet.GetDataSet.ElementAt(0).Key);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void GetDataSetByIndexBadParameters(int index)
        {

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                DataSet dataSet = dataSets.GetDataSet(index);
            });
        }


        [Test]
        [TestCase(Codes.ANALOG)]
        [TestCase(Codes.CONSUMER)]
        public void ExistsInDataSetGoodParameters(Codes code)
        {

            bool exists = dataSets.ExistInDataSet(code);

            Assert.IsTrue(exists);

        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void ExistsInDataSetBadParameters(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bool exists = dataSets.ExistInDataSet(code);
            });
        }


    }
}
