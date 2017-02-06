using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using BufferService.State;
using Common;
using NUnit.Framework;

namespace Tests.BufferServiceTests.State
{
    [TestFixture]
    public class LocalStateTest
    {
        private AState localState = null;

        [SetUp]
        public void SetUp()
        {
            localState = new BufferService.State.LocalState(new BufferModel(), null);
        }
        [Test]
        public void ConstructorWithGoodParameters()
        {

            AState localState = new BufferService.State.LocalState(new BufferModel(), null);
            Assert.AreNotEqual(null, localState);
        }
        [Test]
        public void ConstructorWithNullParameters()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                AState localState = new BufferService.State.LocalState(null, null);
            });
        }

        [Test]
        public void SetRawDataGoodParameter()
        {
            Dictionary<Codes, double> rawData = new Dictionary<Codes, double>(1);
            rawData.Add(Codes.ANALOG, 1);
            localState.SetRawData(rawData);
            Assert.AreEqual(localState.RawData.ElementAt(0).Key, Codes.ANALOG);
        }

        [Test]
        public void SetRawDataNullParameter()
        {
            Assert.Throws<ArgumentNullException>(() => { localState.SetRawData(null); });
        }

    }
}
