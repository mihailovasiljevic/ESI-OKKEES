using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Common.Interfaces;
using Common.Model;
using Connection;


namespace BufferClient
{
    public class Client : IBufferServiceClientContract, IDisposable
    {
        private IClientFactory clientFactory;
        private DBRepository repository = null;

        public DBRepository DBRepository
        {
            get { return repository; }
            set { repository = value; }
        }

        public Client(NetTcpBinding binding, string address, IClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            repository = new DBRepository("localhost", "esi-oikkes", "root", "root");
        }

        public void MeasurementOfDumpingValues(Dictionary<Codes, double> measurements)
        {
            clientFactory.GetService().MeasurementOfDumpingValues(measurements);
        }

        public void ChangeState(bool localState)
        {
            clientFactory.GetService().ChangeState(localState);
        }

        public Dictionary<string, Dictionary<string, double>> GetData(DateTime startDate, DateTime endDate)
        {
            return clientFactory.GetService().GetData(startDate, endDate);
        }

        public SystemConfiguration GetSystemConfiguration()
        {
            return clientFactory.GetService().GetSystemConfiguration();
        }

        public void SetSystemConfiguration(double pmin, double pmax, int deadband)
        {
            if (deadband < 0 || pmax < pmin)
            {
                throw new ArgumentException("Ne mozete ovo da uradite");
            }
            clientFactory.GetService().SetSystemConfiguration(pmin, pmax, deadband);
        }

        public void ConnectToHistorical(string address)
        {
            clientFactory.GetService().ConnectToHistorical(address);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                if (repository != null)
                {
                    repository.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose(false);
        }
    }
}
