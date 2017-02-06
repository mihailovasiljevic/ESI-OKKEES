using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BufferService.Duplex;
using BufferService.State;
using Common;
using Common.Interfaces;
using Common.Model;
using Connection;

namespace BufferService.Contracts
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BufferServiceClientContract : IBufferServiceClientContract
    {

        private BufferModel bufferModel;
        private HistoricalDuplexClient historicalDuplexClient;
        private DBRepository dbRepository;

        public BufferServiceClientContract()
        {
            CreateBufferModel();

        }

        private void CreateBufferModel()
        {
            DataSets dataSets = new DataSets();
            DeltaCD deltaCd = new DeltaCD(dataSets);

            SystemConfiguration systemConfiguration = new SystemConfiguration(-1, -1, -1, States.LOCAL);
            DBRepository dbRepository = new DBRepository("localhost", "esi-oikkes", "root", "root");
            StateContext stateContext = new StateContext(null, dbRepository, systemConfiguration);
            this.bufferModel = new BufferModel(dataSets, deltaCd, systemConfiguration, dbRepository, stateContext);
            this.historicalDuplexClient = null;
            try
            {
                this.historicalDuplexClient =
                    new HistoricalDuplexClient(new InstanceContext(new CallbackHandler(bufferModel)),
                        new NetTcpBinding(), "net.tcp://10.1.212.109:9999/HistoricalService");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            stateContext.State = new LocalState(bufferModel, historicalDuplexClient);
            dbRepository.UpdateSystemConfigurtation(systemConfiguration.Deadband, systemConfiguration.Pmin, systemConfiguration.Pmax);
            dbRepository.ChangeServiceState((int)States.LOCAL);

        }


        public BufferModel GetBufferModel

        {
            get { return bufferModel; }
            set { bufferModel = value; }
        }

        public HistoricalDuplexClient GetHistoricalDuplexClient
        {
            get { return historicalDuplexClient; }
        }
        public void MeasurementOfDumpingValues(Dictionary<Codes, double> measurements)
        {
            Console.WriteLine("Meri vrednosti.");
            if (measurements == null)
            {
                return;
            }

            bufferModel.GetStateContext.State.SetRawData(measurements);

            bufferModel.GetStateContext.Request();

            Console.WriteLine("[CollectionDescriptionsAdd]------------------------------------");
            foreach (CollectionDescription cd in bufferModel.GetDeltaCd.Add)
            {
                foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
                {
                    Console.WriteLine(dp.Code + " : " + dp.DumpingValue);   
                }
            }
            Console.WriteLine("[CollectionDescriptionsAdd]------------------------------------");

            Console.WriteLine("[CollectionDescriptionsUpdate]------------------------------------");
            foreach (CollectionDescription cd in bufferModel.GetDeltaCd.Update)
            {
                foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
                {
                    Console.WriteLine(dp.Code + " : " + dp.DumpingValue);
                }
            }
            Console.WriteLine("[CollectionDescriptionsUpdate]------------------------------------");

        }

        public Dictionary<string, Dictionary<string, double>> GetData(DateTime startDate, DateTime endDate)
        {
            if (startDate.Equals(DateTime.MinValue) && endDate.Equals(DateTime.MaxValue))
            {
                return bufferModel.DbRepository.GetAllDataDumpingProp();
            }
            else
            {
                return bufferModel.DbRepository.GetDataForBufferStatistics(startDate, endDate);  
            }

        }

        public SystemConfiguration GetSystemConfiguration()
        {
            return bufferModel.GetSystemystemConfiguration;
        }

        public void SetSystemConfiguration(double pmin, double pmax, int deadbeand)
        {
            bufferModel.GetSystemystemConfiguration.Pmin = pmin;
            bufferModel.GetSystemystemConfiguration.Pmax = pmax;
            bufferModel.GetSystemystemConfiguration.Deadband = deadbeand;
            bufferModel.DbRepository.UpdateSystemConfigurtation(deadbeand, pmin, pmax);
            Console.WriteLine("[Servis] Postavljena je nova konfiguracija!");
        }

        

        public void ChangeState(bool localState)
        {
            if (localState)
            {
                bufferModel.GetStateContext.State = new LocalState(bufferModel, historicalDuplexClient);
                bufferModel.DbRepository.ChangeServiceState(0); //upisi u bazu
            }
            else
            {
                bufferModel.GetStateContext.State = new RemoteState(bufferModel, historicalDuplexClient);
                bufferModel.DbRepository.ChangeServiceState(1);
            }


        }


        public void ConnectToHistorical(string address)
        {
            try
            {
                this.historicalDuplexClient =
                    new HistoricalDuplexClient(new InstanceContext(new CallbackHandler(bufferModel)),
                        new NetTcpBinding(), address);
                bufferModel.GetStateContext.State.SetHistoricalDuplexClient(this.historicalDuplexClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DBRepository DbRepository
        {
            get { return dbRepository; }
            set { dbRepository = value; }
        }
    }
}
