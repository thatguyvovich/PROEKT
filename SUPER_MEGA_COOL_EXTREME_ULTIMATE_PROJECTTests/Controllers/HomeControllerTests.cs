using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            if (Assert.Equals(Models.Mein_net_<string>.Start(), null))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void AboutTest()
        {
            Mock<Models.Poem> mPoem = new Mock<Models.Poem>();
            Models.WordSet rSet = new Models.WordSet(new string[0]);
            rSet.AddToSet(mPoem.Object);
            Assert.AreEqual(rSet.GetSize(), 2);
        }
    }
}