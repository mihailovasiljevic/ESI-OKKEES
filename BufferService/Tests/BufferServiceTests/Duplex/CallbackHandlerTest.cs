using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using BufferService.Duplex;
using BufferService.State;
using NUnit.Framework;

namespace Tests.BufferServiceTests.Duplex
{
    [TestFixture]
    public class CallbackHandlerTest
    {
        private CallbackHandler callbackHandler = null;

        [SetUp]
        public void SetUp()
        {
            callbackHandler = new CallbackHandler(new BufferModel());
        }

        [Test]
        public void ConstructorWithGoodParameter()
        {
            CallbackHandler callbackHandler = null;
            Assert.DoesNotThrow(() =>
            {
                callbackHandler = new CallbackHandler(new BufferModel());
                Assert.AreNotEqual(null, callbackHandler);
            });
        }
        [Test]
        public void ConstructorWithBadParameter()
        {
            CallbackHandler callbackHandler = null;
            Assert.Throws<ArgumentException>(() =>
            {
                callbackHandler = new CallbackHandler(null);
            });
        }

        [Test]
        [TestCase(10)]
        [TestCase(12)]
        [TestCase(1)]
        public void SetDeadband(int db)
        {
            callbackHandler.GetBufferModel.GetStateContext.State = new RemoteState(callbackHandler.GetBufferModel, null);
            callbackHandler.SetDeadband(db);

            Assert.AreEqual(db, callbackHandler.GetBufferModel.GetSystemystemConfiguration.Deadband);

        }
    }
}
