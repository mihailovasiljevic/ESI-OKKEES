using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace BufferService.State
{
    public class LocalState : AState
    {

        public LocalState(BufferModel bufferModel, HistoricalDuplexClient proxy)
            : base(bufferModel, proxy)
        {
             //proxy.ChangeState("net.tcp://localhost:40001/BufferService", "LOCAL");
        }

        protected override void TemplateMethod() { }
    }
}
