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
    public class CollectionDescriptionTest
    {
        private CollectionDescription collectionDescription = null;
        private DataSet dataSet = null;
        private DumpingPropertyCollection dumpingPropertyCollection = null;
        [SetUp]
        public void SetUp()
        {
            dataSet = new DataSet();
            dataSet.AddPair(Codes.ANALOG, 1);
            dataSet.AddPair(Codes.DIGITAL, 1);

            dumpingPropertyCollection = new DumpingPropertyCollection();
            collectionDescription = new CollectionDescription(1, dataSet, dumpingPropertyCollection);
            dumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 1), collectionDescription);
        }

        [Test]
        [TestCase(1)]
        public void ConstructorWithGoodParameters(int id)
        {
           CollectionDescription collectionDescription = new CollectionDescription(id, dataSet, dumpingPropertyCollection);
           Assert.AreNotEqual(null, collectionDescription);
           Assert.AreEqual(1, id);
        }

    }
}
