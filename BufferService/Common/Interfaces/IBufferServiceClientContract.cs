using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IBufferServiceClientContract
    {
        [OperationContract]
        void MeasurementOfDumpingValues(Dictionary<Codes, double> measurements);

        [OperationContract]
        Dictionary<string, Dictionary<string, double>> GetData(DateTime startDate, DateTime endDate);

        [OperationContract]
        SystemConfiguration GetSystemConfiguration();

        [OperationContract]
        void SetSystemConfiguration(double pmin, double pmax, int deadbeand);

        [OperationContract]
        void ChangeState(bool localState);

        [OperationContract]
        void ConnectToHistorical(string address);
    }
}
