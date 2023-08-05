using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using PokemonAPI.DTOs;
using System.Collections.Generic;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class PokemonSearchItemTests
    {
        [TestMethod]
        public void FromPokemon_MapsCorrectly()
        {
            // Arrange
            Pokemon pokemon = new()
            {
                Name = "Bulbasaur",
                Sprite = "Bulbasaur.png"
            };
            Pokedex pokedex = new()
            {
                Name = "National"
            };
            PokemonEntry entry = new()
            {
                Pokedex = pokedex,
                Number = 1
            };
            pokemon.PokemonEntries.Add(entry);

            PokemonSearchItemDto expectedSearchItem = new()
            {
                Name = pokemon.Name,
                Image = pokemon.Sprite,
                Number = entry.Number
            };

            // Act
            PokemonSearchItemDto actualSearchItem = PokemonSearchItemDto.FromPokemon(pokemon);

            // Assert
            Assert.AreEqual(expectedSearchItem, actualSearchItem);
        }

        [TestMethod]
        public void Equals_FalseIfNull()
        {
            // Assert
            PokemonSearchItemDto firstItem = new()
            {
                Name = "bulbasaur",
                Image = "bulbasaur.png",
                Number = 1
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
            PokemonSearchItemDto firstItem = new()
            {
                Name = "bulbasaur",
                Image = "bulbasaur.png",
                Number = 1
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
            PokemonSearchItemDto firstItem = new()
            {
                Name = "bulbasaur",
                Image = "bulbasaur.png",
                Number = 1
            };

            PokemonSearchItemDto secondItem = new()
            {
                Name = "charmander",
                Image = "bulbasaur.png",
                Number = 1
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
            PokemonSearchItemDto firstItem = new()
            {
                Name = "bulbasaur",
                Image = "bulbasaur.png",
                Number = 1
            };

            PokemonSearchItemDto secondItem = new()
            {
                Name = "bulbasaur",
                Image = "bulbasaur.png",
                Number = 1
            };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }
    }
}
