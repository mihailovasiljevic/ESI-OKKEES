using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BufferService.State;
using Common;
using Common.Interfaces;

namespace BufferService.Duplex
{
    
    public class CallbackHandler : IHistoricalBufferServiceCallback
    {
        private BufferModel bufferModel;

        public CallbackHandler(BufferModel bufferModel)
        {
            if (bufferModel == null)
            {
                throw new ArgumentException("Morate da prosledite buffer model!");
            }
            this.bufferModel = bufferModel;
        }

        public BufferModel GetBufferModel
        {
            get { return bufferModel; }
            set { bufferModel = value; }
        }

        public void SetDeadband(int deadband)
        {
            Console.WriteLine("[CallbackHandler] Setting deadband ");

            if (bufferModel.GetStateContext.State is LocalState)
            {
                Console.WriteLine("Pokusali su da promene stanje dok smo u lokalu!");
                return;
            }
            else
            {
                bufferModel.GetSystemystemConfiguration.Deadband = deadband;
                Console.WriteLine("[CallbackHandler] Deadbeand : {0}", deadband);
                return;
            }
        }

        public bool Ping()
        {
            Console.WriteLine("[CallbackHandler] Ping : Alive!");
            return true;
        }
    }
}
