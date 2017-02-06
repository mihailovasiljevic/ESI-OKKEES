using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using BufferService.Contracts;
using Common;
using Connection;
using NUnit.Framework;

namespace Tests.BufferServiceTests
{
    [TestFixture]
    public class BufferServiceClientContractTest
    {
        private BufferModel bufferModel = null;
        private Dictionary<Codes, double> measurements = null;
        private BufferServiceClientContract bufferServiceClientContract = null;

        [SetUp]
        public void SetUp()
        {
            bufferModel = new BufferModel();
            bufferModel.DbRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            bufferServiceClientContract = new BufferServiceClientContract();
            bufferServiceClientContract.DbRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            measurements = new Dictionary<Codes, double>();
            measurements.Add(Codes.ANALOG, 250);
            measurements.Add(Codes.DIGITAL, 1);
            measurements.Add(Codes.CONSUMER, 125);
            measurements.Add(Codes.CUSTOM, 250);
            //measurements.Add(Codes.SINGLENODE, 300);
            measurements.Add(Codes.MULTIPLENODE, 400);
            measurements.Add(Codes.LIMITSET, 200);
            measurements.Add(Codes.MOTION, 800);
            measurements.Add(Codes.SENSOR, 900);
            measurements.Add(Codes.SOURCE, 1000);
        }

        [Test]
        public void ConstructorWithoutParameters()
        {
            BufferServiceClientContract bufferServiceClientContract = new BufferServiceClientContract();
            Assert.AreNotEqual(bufferServiceClientContract, null);
        }

        [Test]
        public void MeasurementOfDumpingValuesGoodParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                bufferServiceClientContract.MeasurementOfDumpingValues(measurements);
            });
        }


        [Test]
        public void MeasurementOfDumpingValuesBadParameters()
        {
            Assert.DoesNotThrow(() =>
            {
                bufferServiceClientContract.MeasurementOfDumpingValues(null);
            });
        }


    }
}
