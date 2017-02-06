using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using Connection;

namespace BufferService.State
{
    public abstract class AState : IState
    {
        protected BufferModel bufferModel;
        private Dictionary<Codes, double> rawData;
        protected HistoricalDuplexClient proxy;

        protected AState(BufferModel bufferModel, HistoricalDuplexClient proxy)
        {
            if (bufferModel == null)
            {
                throw new ArgumentException("BUffer model ne sme biti null.");
            }

            this.bufferModel = bufferModel;
            this.rawData = null;
            this.proxy = proxy;

            try
            {
                proxy.ChangeState("net.tcp://localhost:40001/BufferService",
                    bufferModel.GetSystemystemConfiguration.State.ToString());
                Console.WriteLine("[STATE]: Sending change of state to historical.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Trying to send change of state to historical: " + ex.Message);
                Console.WriteLine("Probably not connected.");
            }
        }

        public Dictionary<Codes, double> RawData
        {
            get { return rawData; }
            set { rawData = value; }
        }


        protected void FillModel()
        {
            if (rawData == null)
            {
                return;
            }
            foreach (KeyValuePair<Codes, double> raw in rawData)
            {
                bool exists = bufferModel.DbRepository.CheckIfCodeExistDumpProp(raw.Key) || CodeAlreadyInAddList(raw.Key);

                if (exists)
                {
                    AddRaw(bufferModel.GetDeltaCd.Update,
                        raw.Key, raw.Value);


                }
                else
                {
                    AddRaw(bufferModel.GetDeltaCd.Add,
                        raw.Key, raw.Value);

                }
            }
        }

        private void InsertIntoDataBase(Codes code, double value)
        {
            bufferModel.DbRepository.InsertIntoDumpingPropTable(code, value);
        }

        private bool CodeAlreadyInAddList(Codes code)
        {
            foreach (CollectionDescription cd in bufferModel.GetDeltaCd.Add)
            {
                if (cd.DataSet.ExistsInDataSet(code))
                {
                    foreach (DumpingProperty dumpingProperty in cd.DumpingPropertyCollection.DumpingProperties)
                    {
                        if (dumpingProperty.Code == code)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private void AddRaw(List<CollectionDescription> listCollectionDescriptions, Codes rawCode, double rawValue)
        {
            if (!DeadbandOk(rawCode, rawValue))
            {
                return;
            }
            try
            {
                CollectionDescription cd = FindCollectionDescription(listCollectionDescriptions,
                    rawCode);
                if (cd != null)
                {
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(rawCode, rawValue), cd);

                    InsertIntoDataBase(rawCode, rawValue);

                }
            }
            catch (ArgumentException aex)
            {
                Console.WriteLine("[WARNING]: " + aex.Message);
            }

        }

        private bool DeadbandOk(Codes code, double value)
        {
            if (code == Codes.ANALOG)
            {
                return true;
            }
            KeyValuePair<Codes, double> lastAdded = bufferModel.DbRepository.GetFreshDumpValue(code);
            if (lastAdded.Value == -99999f)
            {
                return true;
            }
            else
            {
                Console.WriteLine("[DEADBAND]: " + bufferModel.GetSystemystemConfiguration.Deadband);
                if ((Math.Abs(value - lastAdded.Value) / lastAdded.Value) * 100 <
                    bufferModel.GetSystemystemConfiguration.Deadband)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
        }

        private CollectionDescription FindCollectionDescription(List<CollectionDescription> collectionDescriptions, Codes rawCode)
        {
            
            foreach (CollectionDescription cd in collectionDescriptions)
            {
                if (cd.DataSet.ExistsInDataSet(rawCode))
                {
                    return cd;
                }
            }
            return null;
        }


        protected abstract void TemplateMethod();

        public void Handle(StateContext context)
        {
            TemplateMethod();
            FillModel();
        }

        public void SetRawData(Dictionary<Codes, double> rawData)
        {
            if (rawData == null)
            {
                throw new ArgumentNullException("Parametar ne sme biti null.");
            }

            this.rawData = rawData;
        }

        public HistoricalDuplexClient HistoricalDuplexClient
        {
            get { return proxy; }
            set { proxy = value; }
        }

        public void SetHistoricalDuplexClient(HistoricalDuplexClient hdxClient)
        {
            HistoricalDuplexClient = hdxClient;
        }
    }
}
