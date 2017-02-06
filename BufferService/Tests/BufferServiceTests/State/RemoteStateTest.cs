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
    public class RemoteStateTest
    {
        private RemoteState remoteState;
        [SetUp]
        public void SetUp()
        {
            remoteState = new BufferService.State.RemoteState(new BufferModel(), null);
        }
        [Test]
        public void ConstructorWithGoodParameters()
        {

            RemoteState remoteState = new BufferService.State.RemoteState(new BufferModel(), null);
            Assert.AreNotEqual(null, remoteState);
        }
        [Test]
        public void ConstructorWithNullParameters()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RemoteState remoteState = new BufferService.State.RemoteState(null, null);
            });

        }

        [Test]
        public void SetRawDataGoodParameter()
        {
            Dictionary<Codes, double> rawData = new Dictionary<Codes, double>(1);
            rawData.Add(Codes.ANALOG, 1);
            remoteState.SetRawData(rawData);
            Assert.AreEqual(remoteState.RawData.ElementAt(0).Key, Codes.ANALOG);
        }

        [Test]
        public void SetRawDataNullParameter()
        {
            Assert.Throws<ArgumentNullException>(() => { remoteState.SetRawData(null); });
        }
    }
}
