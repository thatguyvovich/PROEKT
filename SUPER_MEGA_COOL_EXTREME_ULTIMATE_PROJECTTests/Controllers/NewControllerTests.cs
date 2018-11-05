using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECTTests.Controllers
{
    [TestClass()]
    class NewControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            if (Assert.Equals(Config.Start(), null))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void AboutTest()
        {
            Mock<config.Config>  = new Mock<config.Config>();
            Assert.AreEqual(config.getInstance(), null);
        }
    }
}
