using System;
using System.ServiceModel;
using BufferClient;
using Common.Model;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD_ChangeState
{
    [Binding]
    public class ChangeStateSteps
    {
        private Client proxy;
        private int result = -1;
        [Given(@"I have client and service started")]
        public void GivenIHaveClientAndServiceStarted()
        {
            proxy = new Client(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService", new MoqupFactory(new NetTcpBinding(), "net.tcp://localhost:40000/BufferService"));
            result = -1;
        }
        
        [When(@"I have entered (.*)")]
        public void WhenIHaveEntered(int p0)
        {
            if (p0 == 0)
            {
                proxy.ChangeState(true);
            }
            else
            {
                proxy.ChangeState(false);
            }

            SystemConfiguration sc = proxy.GetSystemConfiguration();
            result = (int)sc.State;
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.AreEqual(result, p0);
        }
    }
}
