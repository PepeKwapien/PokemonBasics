using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonAPI.DTOs;
using System.Collections.Generic;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class AbilityDtoTests
    {
        [TestMethod]
        public void Equals_FalseIfNull()
        {
            // Assert
            AbilityDto firstItem = new AbilityDto()
            {
                Name = "stinky",
                Effect = "stinks"
            };

            // Act
            var result = firstItem.Equals(null);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_FalseIfOtherObject()
        {
            // Assert
            AbilityDto firstItem = new AbilityDto()
            {
                Name = "stinky",
                Effect = "stinks"
            };

            // Act
            var result = firstItem.Equals(new List<int>());

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_FalseIfNameDiffers()
        {
            // Assert
            AbilityDto firstItem = new AbilityDto()
            {
                Name = "stinky",
                Effect = "stinks"
            };

            AbilityDto secondItem = new AbilityDto()
            {
                Name = "stinkers",
                Effect = "stinks"
            };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_TrueIfEverythingIsTheSame()
        {
            // Assert
            AbilityDto firstItem = new AbilityDto()
            {
                Name = "stinky",
                Effect = "stinks"
            };

            AbilityDto secondItem = new AbilityDto() { Name = "stinky", Effect = "stinks" };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }
    }
}
