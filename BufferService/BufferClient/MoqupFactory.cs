using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BufferService.Contracts;
using Common.Interfaces;

namespace BufferClient
{
    public class MoqupFactory : ChannelFactory<IBufferServiceClientContract>, IClientFactory
    {
        private IBufferServiceClientContract factory;
        public MoqupFactory(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public override IBufferServiceClientContract CreateChannel(EndpointAddress address, Uri via)
        {
            return new BufferServiceClientContract();
        }

        public IBufferServiceClientContract GetService()
        {
            return factory;
        }

    }
}
