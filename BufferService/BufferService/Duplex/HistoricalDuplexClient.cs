using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using ServiceContract;

namespace BufferService
{
    public class HistoricalDuplexClient : DuplexChannelFactory<IHistoricalBufferService>, IHistoricalBufferService, IDisposable
    {
        private IHistoricalBufferService factory;
        private InstanceContext instanceContext;
        //private BufferModel bufferModel;

        public HistoricalDuplexClient(InstanceContext instanceContext, NetTcpBinding binding, string address)
            : base(instanceContext, binding, address)
        {
            this.instanceContext = instanceContext;
            try
            {
                factory = this.CreateChannel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            // predstavi se
            factory.Connect(address, "LOCAL");
        }
        private static ulong Hash(DateTime when)
        {
            ulong kind = (ulong)(int)when.Kind;
            return (kind << 62) | (ulong)when.Ticks;
        }
        public void Connect(string address, string state)
        {
            try
            {
                factory.Connect(address, state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void TransferDelta(DeltaCD delta)
        {
            try
            {
                factory.TransferDelta(delta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChangeState(string address, string state)
        {
            try
            {
                factory.ChangeState(address, state);
            }            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
