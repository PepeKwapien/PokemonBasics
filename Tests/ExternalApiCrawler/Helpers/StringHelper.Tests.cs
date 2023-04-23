using ExternalApiHandler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ExternalApiHandler.Helpers
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void NormalizesCorrectly()
        {
            // Arrange
            string beforeString = "i-am_not-normal seriously";
            string afterString = "I Am Not Normal Seriously";

            // Act
            var result = StringHelper.Normalize(beforeString);

            // Assert
            Assert.AreEqual(afterString, result);
        }
    }
}
