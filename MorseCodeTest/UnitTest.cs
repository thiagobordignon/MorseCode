using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MorseCode;

namespace MorseCodeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTrue()
        {
            var value = true;
            Assert.AreEqual(value, true);
        }
    }
}
