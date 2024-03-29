﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using PokemonAPI.DTOs;
using System.Collections.Generic;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class PokemonTypeDtoTests
    {
        [TestMethod]
        public void PokemonTypeDto_FromType()
        {
            // Arrange
            PokemonType type = new()
            {
                Name = "Grass",
                Color = "Green"
            };
            PokemonTypeDto expectedDto = new()
            {
                Name = "Grass",
                Color = "Green"
            };

            // Act
            PokemonTypeDto result = PokemonTypeDto.FromPokemonType(type);

            // Assert
            Assert.AreEqual(expectedDto, result);
        }

        [TestMethod]
        public void Equals_FalseIfNull()
        {
            // Assert
            PokemonTypeDto firstItem = new()
            {
                Name = "stinky",
                Color = "purple"
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
            PokemonTypeDto firstItem = new()
            {
                Name = "stinky",
                Color = "purple"
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
            PokemonTypeDto firstItem = new()
            {
                Name = "stinky",
                Color = "purple"
            };

            PokemonTypeDto secondItem = new()
            {
                Name = "stinkers",
                Color = "purple"
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
            PokemonTypeDto firstItem = new()
            {
                Name = "stinky",
                Color = "purple"
            };

            PokemonTypeDto secondItem = new()
            {
                Name = "stinky",
                Color = "purple"
            };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }
    }
}
