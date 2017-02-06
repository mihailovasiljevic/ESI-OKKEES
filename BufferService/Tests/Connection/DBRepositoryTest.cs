using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connection;
using NUnit.Framework;
using BufferClient;
using System.ServiceModel;
using Common;
using Moq;

namespace Tests.Connection
{
    [TestFixture]
    public class DBRepositoryTest
    {
        private DBRepository repository = null;

        [SetUp]
        public void Setup()
        {
            repository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
        }

        [Test]
        [TestCase("localhost", "esi-oikkes-test", "root", "root")]
        public void DBRepositoryGoodConstructor(string server, string database, string uid, string password)
        {
            DBRepository r = new DBRepository(server, database, uid, password);
            Assert.AreNotEqual(r, null);
        }

        [Test]
        public void Dispose()
        {
            Assert.DoesNotThrow(() =>
            {
                repository.Dispose();
            });
        }

        [Test]
        public void GetConnection()
        {
            Assert.AreNotEqual(repository.GetConnection(), null);
        }

        [Test]
        [TestCase(10, 0, 1.0, 15.4)]
        public void InsertIntoSystemConfTableGoodParams(int deadband, int state, double pMin, double pMax)
        {

            Assert.DoesNotThrow(() =>
            {
                repository.InsertIntoSystemConfTable(deadband, state, pMin, pMax);
            }); 
        }

        [Test]
        [TestCase(-1, -2, 15.0, 4.2)]
        [TestCase(-5, 8, 11.0, 8.4)]
        public void InsertIntoSystemConfTableBadParams(int deadband, int state, double pMin, double pMax)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                repository.InsertIntoSystemConfTable(deadband, state, pMin, pMax);
            });    
        }

        [Test]
        [TestCase(Codes.CONSUMER, 14.2)]
        [TestCase(Codes.ANALOG, 5.4)]
        public void InsertIntoDumpingPropTableGoodParam(Codes code, double value)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.InsertIntoDumpingPropTable(code, value);
            }); 
        }

        [Test]
        [TestCase(-1, 14.2)]
        [TestCase(25, 16.2)]
        public void InsertIntoDumpingPropTableBadParam(Codes code, double value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                repository.InsertIntoDumpingPropTable(code, value);
            });  
        }

        [Test]
        [TestCase(7, 1.0, 8.5)]
        public void UpdateSystemConfigurtationGoodParam(int deadband, double pMin, double pMax)
        {
            Assert.DoesNotThrow(() =>
                {
                    repository.UpdateSystemConfigurtation(deadband, pMin, pMax);
                });       
        }

        [Test]
        public void GetAllDataSystemConf()
        {
            Assert.DoesNotThrow(() =>
            {
                repository.GetAllDataSystemConf();
            }); 
 
            Assert.IsNotEmpty(repository.GetAllDataSystemConf());
        }

        [Test]
        public void GetAllDataDumpingProp()
        {
            Assert.DoesNotThrow(() =>
            {
                repository.GetAllDataDumpingProp();
            });

            Assert.IsNotEmpty(repository.GetAllDataDumpingProp());   
        }

        [Test]
        [TestCase(1901, 1, 1, 2200, 12, 31)]
        public void GetDataForBufferStatistics(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            Assert.DoesNotThrow(() =>
            {
                repository.GetDataForBufferStatistics(startDate, endDate);
            });

            Assert.IsNotEmpty(repository.GetDataForBufferStatistics(startDate, endDate));
        }

        [Test]
        [TestCase(1001, 1, 1, 1001, 2, 2)]
        public void GetDataForBufferStatisticsEmpty(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            Assert.DoesNotThrow(() =>
            {
                repository.GetDataForBufferStatistics(startDate, endDate);
            });

            Assert.IsEmpty(repository.GetDataForBufferStatistics(startDate, endDate));
        }

        [Test]
        [TestCase(Codes.ANALOG)]
        public void CheckIfCodeExistDumpProp(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.CheckIfCodeExistDumpProp(code);
            });

            Assert.IsTrue(repository.CheckIfCodeExistDumpProp(code)); 
        }

        [Test]
        [TestCase(Codes.SINGLENODE)]
        public void CheckIfCodeNoExistDumpProp(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.CheckIfCodeExistDumpProp(code);
            });

            Assert.IsFalse(repository.CheckIfCodeExistDumpProp(code));
        }

        [Test]
        [TestCase(-4)]
        [TestCase(27)]
        public void CheckIfFalseCodeExistDumpProp(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                repository.CheckIfCodeExistDumpProp(code);
            });
        }

        [Test]
        [TestCase(Codes.ANALOG)]
        public void GetFreshDumpValueGoodParam(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.GetFreshDumpValue(code);
            });   
  
            //Assert.AreNotEqual(repository.GetFreshDumpValue(code), null);
            Assert.AreNotEqual(repository.GetFreshDumpValue(code), new KeyValuePair<Codes, double>());
        }

        [Test]
        [TestCase(-4)]
        [TestCase(41)]
        public void GetFreshDumpValueBadParam(Codes code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                repository.GetFreshDumpValue(code);
            });
        }

        [Test]
        [TestCase(Codes.SINGLENODE)]
        public void GetFreshNoDumpValue(Codes code)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.GetFreshDumpValue(code);
            });

            Assert.AreEqual(repository.GetFreshDumpValue(code), new KeyValuePair<Codes, double>(Codes.ANALOG, -99999));
        }

        [Test]
        [TestCase(0)]
        public void ChangeServiceLocalState(int localState)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.ChangeServiceState(localState);
            }); 
        }

        [Test]
        [TestCase(1)]
        public void ChangeServiceRemoteState(int remoteState)
        {
            Assert.DoesNotThrow(() =>
            {
                repository.ChangeServiceState(remoteState);
            });
        }
    }
}
