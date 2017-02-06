using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.ServiceModel;
using Connection;

namespace Tests.Connection
{
    [TestFixture]
    public class ConnectionTest
    {
        private global::Connection.Connection conn = null;

        [SetUp]
        public void Setup()
        {
            conn = new global::Connection.Connection();
        }

        [Test]
        public void ConnectionKonstruktor()
        {
            global::Connection.Connection c = new global::Connection.Connection();
            Assert.AreNotEqual(c, null);
        }

        [Test]
        public void Open()
        {
            Assert.DoesNotThrow(() =>
            {
                conn.Open();
            });     
        }

        [Test]
        public void Close()
        {
            Assert.DoesNotThrow(() =>
            {
                conn.Close();
            });
        }
    }
}
