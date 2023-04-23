using ExternalApiCrawler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using System;

namespace Tests.ExternalApiHandler.Helpers
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
            string key = "";

            // Act
            

            // Assert
            Assert.ThrowsException<ArgumentException>(()=>EnumHelper.GetEnumValueFromKey<Regions>(key));
        }

        [TestMethod]
        public void ThrowsErrorIfKeyDoesNotExist()
        {
            // Arrange
            string key = "poland";

            // Act


            // Assert
            Assert.ThrowsException<ArgumentException>(() => EnumHelper.GetEnumValueFromKey<Regions>(key));
        }
    }
}
