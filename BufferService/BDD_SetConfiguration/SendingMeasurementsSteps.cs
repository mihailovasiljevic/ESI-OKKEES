using System;
using System.Collections.Generic;
using System.ServiceModel;
using BufferClient;
using Common;
using Connection;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD_SetConfiguration
{
    [Binding]
    public class SendingMeasurementsSteps
    {
        private BufferClient.Client proxy;
        private Dictionary<Codes, double> dict;
        private DBRepository repo;

        [Given(@"I have my client and service started")]
        public void GivenIHaveMyClientAndServiceStarted()
        {
            proxy = new Client(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService", new MoqupFactory(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService"));
            proxy.SetSystemConfiguration(10, 100, 10);
            repo = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            //repo.DeleteEverything("esi-oikkes-test");
            dict = new Dictionary<Codes, double>(1);
        }
        
        [When(@"I have entered (.*) and (.*)")]
        public void WhenIHaveEnteredAnd(int p0, Decimal p1)
        {
            Codes code;
            if (p0 == 0)
            {
                code = Codes.ANALOG;
            }
            else
            {
                code = Codes.DIGITAL;
            }

            dict.Add(code, (double)p1);
            proxy.MeasurementOfDumpingValues(dict);
        }
        
        [Then(@"the result of (.*) should be (.*)")]
        public void ThenTheResultOfShouldBe(int p0, Decimal p1)
        {
            Codes code;
            if (p0 == 0)
            {
                code = Codes.ANALOG;
            }
            else
            {
                code = Codes.DIGITAL;
            }
            KeyValuePair<Codes, double> pair = repo.GetFreshDumpValue(code);
            Assert.AreEqual(pair.Value, (double)p1);
        }
    }
}
