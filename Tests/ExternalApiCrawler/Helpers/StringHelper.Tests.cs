using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
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

        [TestMethod]
        public void NormalizesNamesArrayCorrectly()
        {
            // Arrange
            Name[] names = new Name[]
            {
                new Name
                {
                    name = "bacon-pancakes"
                },
                new Name
                {
                    name = "making_bacon pancakes"
                },
            };

            string expectedString = "Bacon Pancakes, Making Bacon Pancakes";

            // Act
            var result = StringHelper.NormalizeAndJoinNames(names);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedString, result);
        }
    }
}
