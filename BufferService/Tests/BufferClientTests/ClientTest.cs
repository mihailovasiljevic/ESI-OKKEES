using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BufferClient;
using Common;
using Connection;


namespace Tests.BufferClientTests
{
    [TestFixture]
    public class ClientTest
    {
        private IClientFactory clientFactory = null;
        //private DBRepository repository = null;
        private NetTcpBinding binding = null;
        private Client client = null;
        private string address = string.Empty;
        private static Dictionary<Codes, double> measurements = null;

        [SetUp]
        public void Setup()
        {
            binding = new NetTcpBinding();
            address = "net.tcp://localhost:40000/BufferService";
            //repository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            clientFactory = new MoqupFactory(binding, address);
            client = new Client(binding, address, clientFactory);
            client.DBRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");

            measurements = new Dictionary<Codes, double>();
            measurements.Add(Codes.ANALOG, 250);
            measurements.Add(Codes.DIGITAL, 1);
            measurements.Add(Codes.CONSUMER, 125);
            measurements.Add(Codes.CUSTOM, 250);
            measurements.Add(Codes.MULTIPLENODE, 400);
            measurements.Add(Codes.LIMITSET, 200);
            measurements.Add(Codes.MOTION, 800);
            measurements.Add(Codes.SENSOR, 900);
            measurements.Add(Codes.SOURCE, 1000);
        }

        [Test]
        public void ClientConstructor()
        {
            client = new Client(binding, address, clientFactory);
            Assert.AreNotEqual(client, null);  
        }

        [Test]
        public void MeasurementOfDumpingValuesGoodParam()
        {
            Assert.DoesNotThrow(() =>
            {
                client.MeasurementOfDumpingValues(measurements);
            });
        }

        [Test]
        public void MeasurementOfDumpingValuesBadParam()
        {
            Assert.DoesNotThrow(() =>
            {
                client.MeasurementOfDumpingValues(null);
            });
        }

        [Test]
        [TestCase(true)]
        public void ChangeStateLocal(bool localState)
        {
            Assert.DoesNotThrow(() =>
            {
                client.ChangeState(localState);
            });  
        }

        [Test]
        [TestCase(false)]
        public void ChangeStateRemote(bool remoteState)
        {
            Assert.DoesNotThrow(() =>
            {
                client.ChangeState(remoteState);
            });
        }

        [Test]
        [TestCase(0001, 1, 1, 9999, 12, 31)]
        public void GetAllData(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            Assert.DoesNotThrow(() =>
            {
                client.GetData(startDate, endDate);
            });

            Assert.IsNotEmpty(client.GetData(startDate, endDate));
        }

        [Test]
        [TestCase(2001, 1, 1, 2021, 12, 31)]
        public void GetDataBufferStatistics(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            Assert.DoesNotThrow(() =>
            {
                client.GetData(startDate, endDate);
            });

            Assert.IsNotEmpty(client.GetData(startDate, endDate));
        }

        [Test]
        [TestCase(1221, 2, 2, 1221, 3, 3)]
        public void GetEmptyDataBufferStatistics(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            Assert.DoesNotThrow(() =>
            {
                client.GetData(startDate, endDate);
            });

            Assert.IsEmpty(client.GetData(startDate, endDate));
        }

        [Test]
        public void GetSystemConfiguration()
        {
            Assert.AreNotEqual(client.GetSystemConfiguration(), null);
        }

        [Test]
        [TestCase(1.0, 25.0, 6)]
        [TestCase(2.5, 15.2, 9)]
        public void SetSystemConfigurationGoodParams(double pMin, double pMax, int deadband)
        {
            Assert.DoesNotThrow(() =>
                {
                    client.SetSystemConfiguration(pMin, pMax, deadband);
                });
        }

        [Test]
        [TestCase(10.0, 5.0, -1)]
        [TestCase(26.5, 15.2, -3)]
        public void SetSystemConfigurationBadParams(double pMin, double pMax, int deadband)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                client.SetSystemConfiguration(pMin, pMax, deadband);
            });
        }

        [Test]
        [TestCase("net.tcp://localhost:40000/BufferService")]
        public void ConnectToHistorical(string address)
        {
            Assert.DoesNotThrow(() =>
            {
                client.ConnectToHistorical(address);
            }); 
        }

        [Test]
        public void Dispose()
        {
            Assert.DoesNotThrow(() =>
            {
                client.Dispose();
            }); 
        }


    }
}
