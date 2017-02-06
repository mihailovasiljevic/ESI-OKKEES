using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BufferClient;
using Common.Interfaces;

namespace Tests.BufferClientTests
{
    [TestFixture]
    public class RealFactoryTest
    {
        //private IBufferServiceClientContract factory;
        private NetTcpBinding binding = null;
        private string address = string.Empty;
        private IClientFactory clientFactory = null;

        [SetUp]
        public void Setup()
        {
            binding = new NetTcpBinding();
            address = "net.tcp://localhost:40000/BufferService";
            clientFactory = new RealFactory(binding, address);
        }

        [Test]
        public void RealFactoryConstructor()
        {
            clientFactory = new RealFactory(binding, address);
            Assert.AreNotEqual(clientFactory, null); 
        }

        [Test]
        public void GetService()
        {
            Assert.AreNotEqual(clientFactory.GetService(), null);
        }
    }
}
