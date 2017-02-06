using System;
using System.ServiceModel;
using BufferClient;
using Common.Model;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD_SetConfiguration
{
    [Binding]
    public class SetSystemConfigurationFeatureSteps
    {
        private Client proxy;
        private double result = 0;

        [Given(@"I have client and service started")]
        public void GivenIHaveClientAndServiceStarted()
        {
            proxy = new Client(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService", new MoqupFactory(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService"));
            result = 0;
        }

        [When(@"I have entered (.*) as a deadband, (.*) as a pmin, (.*) as a pmax")]
        public void WhenIHaveEnteredAsADeadbandAsAPminAsAPmax(int p0, Decimal p1, Decimal p2)
        {
            proxy.SetSystemConfiguration((double)p1, (double)p2, p0);
            SystemConfiguration sc = proxy.GetSystemConfiguration();
            result += sc.Deadband;
            result += sc.Pmax;
            result += sc.Pmin;
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(Decimal p0)
        {
            Assert.AreEqual(result, p0);
        }
    }
}
