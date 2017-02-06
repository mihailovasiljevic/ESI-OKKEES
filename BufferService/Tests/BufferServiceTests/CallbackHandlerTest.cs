using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BufferService;
using BufferService.Duplex;
using Connection;

namespace Tests.BufferServiceTests
{
    [TestFixture]
    public class CallbackHandlerTest
    {
        private BufferModel bufferModel = null;
        [SetUp]
        public void Setup()
        {
            bufferModel = new BufferModel();
            bufferModel.DbRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
        }

        [Test]
        public void CallbackHandlerKonstruktor()
        {
            CallbackHandler ch = new CallbackHandler(bufferModel);
            Assert.AreNotEqual(ch, null);
        }

        [Test]
        public void Ping()
        {
            CallbackHandler ch = new CallbackHandler(bufferModel);
            Assert.AreEqual(ch.Ping(), true);
        }
    }
}
