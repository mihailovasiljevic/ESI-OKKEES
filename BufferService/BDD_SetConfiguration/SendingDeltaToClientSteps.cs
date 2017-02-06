using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BufferClient;
using BufferService.Contracts;
using BufferService.State;
using Common;
using Common.Model;
using Connection;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD_SetConfiguration
{
    [Binding]
    public class SendingDeltaToClientSteps
    {
        private BufferServiceClientContract bufferServiceClientContract;

        [Given(@"I have my client and service started and their historical should be started")]
        public void GivenIHaveMyClientAndServiceStartedAndTheirHistoricalShouldBeStarted()
        {
            bufferServiceClientContract = new BufferServiceClientContract();
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Deadband = 10;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Pmin = 10;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Pmax = 100;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.State = States.LOCAL;
            bufferServiceClientContract.GetBufferModel.GetStateContext.State = new LocalState(bufferServiceClientContract.GetBufferModel, bufferServiceClientContract.GetHistoricalDuplexClient);
        }
        
        [When(@"I have prepared bufferModel\.deltaCd")]
        public void WhenIHavePreparedBufferModel_DeltaCd()
        {
            foreach (CollectionDescription cd in bufferServiceClientContract.GetBufferModel.GetDeltaCd.Update)
            {
                if (cd.DataSet.ExistsInDataSet(Codes.ANALOG))
                {
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 1), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 2), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 3), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 4), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 5), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 6), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 7), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 8), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 9), cd);
                    cd.DumpingPropertyCollection.AddDumpingProperty(new DumpingProperty(Codes.ANALOG, 10), cd);
                }
            }
        }
        
        [Then(@"the result should be empty deltaCd collections and not throwing exceptions except when historical is not working")]
        public void ThenTheResultShouldBeEmptyDeltaCdCollectionsAndNotThrowingExceptionsExceptWhenHistoricalIsNotWorking()
        {

            try
            {
                bufferServiceClientContract.GetBufferModel.GetStateContext.State = new RemoteState(bufferServiceClientContract.GetBufferModel, bufferServiceClientContract.GetHistoricalDuplexClient);
                bufferServiceClientContract.GetBufferModel.GetStateContext.Request();
                Console.WriteLine("[Try] Uspelo!");
                Assert.AreEqual(0, bufferServiceClientContract.GetBufferModel.GetDeltaCd.Add.ElementAt(0).DumpingPropertyCollection.DumpingProperties.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Catch] Uspelo!" + ex.Message);
                Assert.AreEqual(0, 0);
                throw;
            }


        }
    }
}
