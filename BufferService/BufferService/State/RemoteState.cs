using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace BufferService.State
{
    public class RemoteState : AState
    {
        //private static int transactionId = 0;
        private DeltaCD deltaCd;

        public RemoteState(BufferModel bufferModel, HistoricalDuplexClient proxy)
            : base(bufferModel, proxy)
        {
            //proxy.ChangeState("net.tcp://localhost:40001/BufferService", "REMOTE");
        }

        protected override void TemplateMethod()
        {
            if (GetNumberOfMeasurements() > 10)
            {
                SendDataToHistorical();
                Console.WriteLine("");
            }
        }

        private int GetNumberOfMeasurements()
        {
            int retVal = 0;
            for (int i = 0; i < 5; i++)
            {
                retVal += bufferModel.GetDeltaCd.Add[i].DumpingPropertyCollection.DumpingProperties.Count;
                retVal += bufferModel.GetDeltaCd.Update[i].DumpingPropertyCollection.DumpingProperties.Count;
            }
            return retVal;
        }
        private void SendDataToHistorical()
        {
            CreateDelta();
            /////
            Console.WriteLine("!!!!!!!!!!!New delta update!!!!!!!!!!!!!!!");
            foreach (CollectionDescription cd in deltaCd.Update)
            {
                foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
                {
                    Console.WriteLine("[" + dp.Code + "] : " + dp.DumpingValue);
                }
            }
            Console.WriteLine("!!!!!!!!!!!New delta!!!!!!!!!!!!!!!");

            Console.WriteLine("!!!!!!!!!!!New delta add!!!!!!!!!!!!!!!");
            foreach (CollectionDescription cd in deltaCd.Add)
            {
                foreach (DumpingProperty dp in cd.DumpingPropertyCollection.DumpingProperties)
                {
                    Console.WriteLine("[" + dp.Code + "] : " + dp.DumpingValue);
                }
            }
            Console.WriteLine("!!!!!!!!!!!New delta!!!!!!!!!!!!!!!");
            if(proxy!=null)
                proxy.TransferDelta(deltaCd);
            /////
            ClearCollections();
        }

        private void CreateDelta()
        {
            this.deltaCd = new DeltaCD(bufferModel.GetDataSets);

            this.deltaCd.Add = bufferModel.GetDeltaCd.Add;

            for (int j = 0; j < bufferModel.GetDeltaCd.Update.Count; j++)
            {
                for (int i = 0; i < bufferModel.GetDeltaCd.Update.ElementAt(j).DumpingPropertyCollection.DumpingProperties.Count; i++)
                {
                    if (this.deltaCd.Update.ElementAt(j).DumpingPropertyCollection.DumpingProperties.Count > 0)
                    {
                        bool found = false;

                        for (int k = 0;
                            k < this.deltaCd.Update.ElementAt(j).DumpingPropertyCollection.DumpingProperties.Count;
                            k++)
                        {
                            if (
                                this.deltaCd.Update.ElementAt(j)
                                    .DumpingPropertyCollection.DumpingProperties.ElementAt(k)
                                    .Code ==
                                bufferModel.GetDeltaCd.Update.ElementAt(j)
                                    .DumpingPropertyCollection.DumpingProperties.ElementAt(i)
                                    .Code)
                            {
                                found = true;
                                this.deltaCd.Update.ElementAt(j)
                                        .DumpingPropertyCollection.DumpingProperties.ElementAt(k).DumpingValue =
                                    bufferModel.GetDeltaCd.Update.ElementAt(j)
                                        .DumpingPropertyCollection.DumpingProperties.ElementAt(i).DumpingValue;
                                break;
                            }
                        }

                        if (!found)
                        {
                            this.deltaCd.Update.ElementAt(j)
                                        .DumpingPropertyCollection.AddDumpingProperty(bufferModel.GetDeltaCd.Update.ElementAt(j)
                                        .DumpingPropertyCollection.DumpingProperties.ElementAt(i), bufferModel.GetDeltaCd.Update.ElementAt(j));
                        }

                    }
                    else
                    {
                        this.deltaCd.Update.ElementAt(j)
                                    .DumpingPropertyCollection.AddDumpingProperty(bufferModel.GetDeltaCd.Update.ElementAt(j)
                                    .DumpingPropertyCollection.DumpingProperties.ElementAt(i), bufferModel.GetDeltaCd.Update.ElementAt(j));
                    }
                }
            }

            
            
        }

        private void ClearCollections()
        {
           deltaCd = bufferModel.GetDeltaCd;
           deltaCd.ClearCollectionDescriptions(deltaCd.Add);
           deltaCd.ClearCollectionDescriptions(deltaCd.Update);

           deltaCd.TransactionId++;
        }
    }
}
