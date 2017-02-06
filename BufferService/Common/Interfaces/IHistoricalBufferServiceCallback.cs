using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IHistoricalBufferServiceCallback
    {
        [OperationContract]
        void SetDeadband(int deadband);
        [OperationContract]
        bool Ping();
    }
}
