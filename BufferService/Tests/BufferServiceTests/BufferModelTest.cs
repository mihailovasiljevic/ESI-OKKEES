using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferService;
using Connection;
using NUnit.Framework;
namespace Tests.BufferServiceTests
{
    [TestFixture]
    public class BufferModelTest
    {
        //private BufferModel bufferModel = null;

        [Test]
        public void ConstructorWithoutParameters()
        {
            BufferModel bm = new BufferModel();
            bm.DbRepository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            Assert.AreNotEqual(bm, null);
            Assert.AreEqual(bm.GetDeltaCd.Add.Count, 5);
            Assert.AreEqual(bm.GetSystemystemConfiguration.Deadband, -1);
        }


    }
}
