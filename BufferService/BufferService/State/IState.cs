using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BufferService.State
{
    public interface IState
    {

        void Handle(StateContext context);
        void SetRawData(Dictionary<Codes, double> rawData);

        void SetHistoricalDuplexClient(HistoricalDuplexClient hdxClient);
    }
}
