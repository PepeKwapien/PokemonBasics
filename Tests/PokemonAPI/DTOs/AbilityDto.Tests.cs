using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
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

        [TestMethod]
        public void FromAbility_MapsCorrectly()
        {
            // Arrange
            Ability ability = new()
            {
                Name = "stinky",
                Effect = "stinks"
            };
            AbilityDto expected = new()
            {
                Name = ability.Name,
                Effect= ability.Effect,
            };

            // Act
            var result = AbilityDto.FromAbility(ability);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FromPokemonAbility_MapsCorrectly()
        {
            // Arrange
            PokemonAbility pokemonAbility = new()
            {
                Ability = new()
                {
                    Name = "stinky",
                    Effect = "stinks"
                },

            };
            AbilityDto expected = new()
            {
                Name = pokemonAbility.Ability.Name,
                Effect = pokemonAbility.Ability.Effect,
            };

            // Act
            var result = AbilityDto.FromPokemonAbility(pokemonAbility);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
