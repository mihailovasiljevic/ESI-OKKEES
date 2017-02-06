using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace BufferClient
{
    public interface IClientFactory
    {
        IBufferServiceClientContract GetService();
    }
}
