using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using NUnit.Framework;

namespace Tests.Model
{
    [TestFixture]
    public class SystemConfigurationTest
    {
        private SystemConfiguration systemConfiguration = null;

        [SetUp]
        public void SetUp()
        {
            systemConfiguration = new SystemConfiguration(-1, -1, -1, States.LOCAL);
        }

        [Test]
        [TestCase(10, 10, 100, States.LOCAL)]
        [TestCase(15, 12, 120, States.LOCAL)]
        public void ConstructorWithGoodParameters(int deadband, double pmin, double pmax, Common.States state)
        {
            SystemConfiguration systemConfiguration = new SystemConfiguration(deadband, pmin, pmax, state);
            Assert.AreNotEqual(null, systemConfiguration);
            Assert.AreEqual(systemConfiguration.State, States.LOCAL);
        }

        [Test]
        [TestCase(10, 10, 100, -1)]
        [TestCase(15, 12, 120, 100)]
        public void ConstructorWithBadParameters(int deadband, double pmin, double pmax, States state)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                SystemConfiguration systemConfiguration = new SystemConfiguration(deadband, pmin, pmax, state);
            });
        }

    }
}
