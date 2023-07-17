using ExternalApiCrawler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using System;

namespace Tests.ExternalApiCrawler.Helpers
{
    [TestClass]
    public class EnumHelperTests
    {
        [TestMethod]
        public void FindsCorrectValue()
        {
            // Arrange
            string key = "johto";

            // Act
            var region = EnumHelper.GetEnumValueFromKey<Regions>(key);

            // Assert
            Assert.IsNotNull(region);
            Assert.AreEqual(Regions.Johto, region);
        }

        [TestMethod]
        public void ThrowsErrorIfEmptyString()
        {
            // Arrange

            // Act

            // Assert
            var exception = Assert.ThrowsException<ArgumentException>(()=>EnumHelper.GetEnumValueFromKey<Regions>(""));
            Assert.AreEqual("The key cannot be null or empty", exception.Message);
        }

        [TestMethod]
        public void ThrowsErrorIfKeyDoesNotExist()
        {
            // Arrange
            string key = "poland";

            // Act

            // Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => EnumHelper.GetEnumValueFromKey<Regions>(key));
            Assert.AreEqual($"No enum value found for key {key}", exception.Message);
        }
    }
}
