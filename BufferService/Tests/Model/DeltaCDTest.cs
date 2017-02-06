using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using Common;
using Common.Model;
using NUnit.Framework;

namespace Tests.Model
{
    [TestFixture]
    public class DeltaCDTest
    {
        private DeltaCD deltaCd = null;

        [SetUp]
        public void SetUp()
        {
            deltaCd = new DeltaCD(new DataSets());
        }

        [Test]
        public void ConstructorWithOneGoodParemeter()
        {
            DeltaCD deltaCd = new DeltaCD(new DataSets());
            Assert.AreNotEqual(deltaCd, null);
        }

        [Test]
        public void ConstructorWithOneBadParemeter()
        {
            DeltaCD deltaCd = new DeltaCD(null);
            Assert.AreNotEqual(deltaCd.SetDataSet, null);
        }

        [Test]
        public void ConstructorWithMoreGoodParemeters()
        {
            DeltaCD deltaCd = new DeltaCD(1, new List<CollectionDescription>(5), new List<CollectionDescription>(5));
            Assert.AreNotEqual(deltaCd, null);
        }

        [Test]
        public void ConstructorWithMoreBadParemeters()
        {
            DeltaCD deltaCd = new DeltaCD(-1, null, null);
            Assert.AreNotEqual(deltaCd.Add, null);
        }

        [Test]
        public void ClearCollectionDescriptionsGoodParameter()
        {
            deltaCd.Add.Add(new CollectionDescription(1, new DataSet(), new DumpingPropertyCollection()));

            deltaCd.ClearCollectionDescriptions(deltaCd.Add);

            Assert.AreEqual(0, deltaCd.Add.ElementAt(0).DumpingPropertyCollection.DumpingProperties.Count);

        }

        public void ClearCollectionDescriptionsBadParameter()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                deltaCd.ClearCollectionDescriptions(null);
            });
        }
    }
}
