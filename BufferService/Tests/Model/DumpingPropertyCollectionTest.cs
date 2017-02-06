using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using Moq;
using NUnit.Framework;

namespace Tests.Model
{
    [TestFixture]
    public class DumpingPropertyCollectionTest
    {
        private DumpingPropertyCollection dumpingPropertyCollection;
        private List<DumpingProperty> dumpingProperties;
        private DumpingProperty moqDumpingProperty;
        private CollectionDescription moqCollectionDescription;
        private DataSet moqDataSet;
        [SetUp]
        public void SetUp()
        {
            dumpingProperties = new List<DumpingProperty>(2);
            dumpingProperties.Add(new DumpingProperty(Codes.ANALOG, 1));
            this.dumpingPropertyCollection = new DumpingPropertyCollection(dumpingProperties);

            moqDumpingProperty = new DumpingProperty(Codes.DIGITAL, 1);
            moqDataSet = new DataSet();
            moqDataSet.AddPair(Codes.DIGITAL, 1);
            moqDataSet.AddPair(Codes.ANALOG, 1);
            moqCollectionDescription = new CollectionDescription(1, moqDataSet, dumpingPropertyCollection);
        }

        [Test]
        public void ConstructorWithNoParameters()
        {
            DumpingPropertyCollection dpc = new DumpingPropertyCollection();
            Assert.AreNotEqual(null, dumpingPropertyCollection);
        }

        [Test]
        public void ConstuructorWithGoodParameters()
        {
            DumpingPropertyCollection dpc = new DumpingPropertyCollection(dumpingProperties);
            Assert.AreNotEqual(null, dpc);
            Assert.AreEqual(1, dpc.DumpingProperties.ElementAt(0).DumpingValue);
        }

        [Test]
        public void AddDumpingPropertiesGoodParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                dumpingPropertyCollection.AddDumpingProperty(moqDumpingProperty, moqCollectionDescription);
            });
        }

        [Test]
        public void AddDumpingPropertiesNullParameters()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                dumpingPropertyCollection.AddDumpingProperty(null, null);
            });
        }

        [Test]
        public void AddDumpingPropertiesWrongDataSetParameters()
        {
            moqDumpingProperty.Code = Codes.CONSUMER;
            Assert.Throws<ArgumentException>(() =>
            {
                dumpingPropertyCollection.AddDumpingProperty(moqDumpingProperty, moqCollectionDescription);
            });
        }
    }
}
