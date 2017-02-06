using System;
using System.Collections.Generic;
using System.Linq;
using BufferService.Contracts;
using BufferService.State;
using Common;
using Connection;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD_SetConfiguration
{
    [Binding]
    public class MakingDeltaSteps
    {
        private BufferServiceClientContract bufferServiceClientContract;

        [Given(@"I have service object and data received from client")]
        public void GivenIHaveServiceObjectAndDataReceivedFromClient()
        {
            bufferServiceClientContract = new BufferServiceClientContract();
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Deadband = 10;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Pmin = 10;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.Pmax = 100;
            bufferServiceClientContract.GetBufferModel.GetSystemystemConfiguration.State = States.LOCAL;
            bufferServiceClientContract.GetBufferModel.GetStateContext.State = new LocalState(bufferServiceClientContract.GetBufferModel, bufferServiceClientContract.GetHistoricalDuplexClient);
            bufferServiceClientContract.GetBufferModel.DbRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            bufferServiceClientContract.GetBufferModel.DbRepository.DeleteEverything("dumping_property");
            bufferServiceClientContract.GetBufferModel.DbRepository.InsertIntoDumpingPropTable(Codes.DIGITAL, 150);

        }
        
        [When(@"I have added element for addition and element for update")]
        public void WhenIHaveAddedElementForAdditionAndElementForUpdate()
        {
            Dictionary<Codes, double> rawData = new Dictionary<Codes, double>(2);
            rawData.Add(Codes.ANALOG, 1); // for add
            rawData.Add(Codes.DIGITAL, 100); // for update
            bufferServiceClientContract.GetBufferModel.GetStateContext.State.SetRawData(rawData);
        }
        
        [Then(@"the collection should be updated")]
        public void ThenTheCollectionShouldBeUpdated()
        {
            bufferServiceClientContract.GetBufferModel.GetStateContext.State.Handle(bufferServiceClientContract.GetBufferModel.GetStateContext);
            Assert.AreEqual(1, bufferServiceClientContract.GetBufferModel.GetDeltaCd.Add.ElementAt(0).DumpingPropertyCollection.DumpingProperties.Count);
            Assert.AreEqual(1, bufferServiceClientContract.GetBufferModel.GetDeltaCd.Update.ElementAt(0).DumpingPropertyCollection.DumpingProperties.Count);
        }
    }
}
