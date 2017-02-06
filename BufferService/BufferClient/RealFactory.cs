using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;
using Common.Model;

namespace BufferClient
{
    public class RealFactory : ChannelFactory<IBufferServiceClientContract>, IClientFactory
    {
        private IBufferServiceClientContract factory;
        public RealFactory(NetTcpBinding binding, string address)
            : base(binding, address)
        {
            factory = this.CreateChannel();

        }
        public Common.Interfaces.IBufferServiceClientContract GetService()
        {
            return factory;
        }

    }
}
