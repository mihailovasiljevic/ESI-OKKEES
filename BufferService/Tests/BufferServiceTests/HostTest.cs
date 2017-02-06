using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.ServiceModel;
using BufferService;
using BufferService.Contracts;
using Common.Interfaces;

namespace Tests.BufferServiceTests
{
    [TestFixture]
    public class HostTest
    {
        private string address = string.Empty;
        private NetTcpBinding binding;
        private Type endPointType;
        private Type serviceType;
        private ServiceHost serviceHost;
        private Host host;

        [SetUp]
        public void Setup()
        {
            address = "net.tcp://localhost:40000/BufferService";
            endPointType = typeof(IBufferServiceClientContract);
            serviceType = typeof(BufferServiceClientContract);
            binding = new NetTcpBinding();
            host = new Host(address, typeof(IBufferServiceClientContract), typeof(BufferServiceClientContract));
        }

        [Test]
        public void HostConsructor()
        {
            host = new Host(address, typeof(IBufferServiceClientContract), typeof(BufferServiceClientContract));
            Assert.AreNotEqual(host, null);
        }

        [Test]
        public void InitializeHost()
        {
            Assert.DoesNotThrow(() =>
            {
                serviceHost = new ServiceHost(serviceType);
            });
            Assert.AreNotEqual(host, null);     
        }

        

        [Test]
        public void Dispose()
        {
            Assert.DoesNotThrow(() =>
            {
                host.Dispose();
            });
        }

    }
}
