using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonAPI.DTOs;
using PokemonAPI.Models;
using System.Collections.Generic;

namespace Tests.PokemonAPI.Models
{
    [TestClass]
    public class PokemonDefensiveCharacteristicsTests
    {
        [TestMethod]
        public void Equals_FalseIfNull()
        {
            // Assert
            PokemonDefensiveCharacteristics firstItem = new();

            // Act
            var result = firstItem.Equals(null);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_FalseIfOtherObject()
        {
            // Assert
            PokemonDefensiveCharacteristics firstItem = new();

            // Act
            var result = firstItem.Equals(new List<int>());

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_TrueIfAllCollectionsAreEmpty()
        {
            // Assert
            PokemonDefensiveCharacteristics firstItem = new();

            PokemonDefensiveCharacteristics secondItem = new();

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_FalseIfAllCollectionsDiffer()
        {
            // Assert
            PokemonDefensiveCharacteristics firstItem = new();

            PokemonDefensiveCharacteristics secondItem = new()
            {
                No = new List<PokemonTypeDto>()
                {
                    new PokemonTypeDto()
                    {
                        Name = "Ghost",
                        Color = "Purple"
                    }
                }
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
            PokemonDefensiveCharacteristics firstItem = new()
            {
                No = new List<PokemonTypeDto>()
                {
                    new PokemonTypeDto()
                    {
                        Name = "Ghost",
                        Color = "Purple"
                    }
                },
                Quarter = new()
                {
                    new()
                    {
                        Name = "Grass",
                        Color = "Green"
                    }
                },
                Half = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Babyblue"
                    }
                },
                Neutral = new()
                {
                    new()
                    {
                        Name = "Normal",
                        Color = "Bronze"
                    }
                },
                Double = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                Quadruple = new()
                {
                    new()
                    {
                        Name = "Fairy",
                        Color = "Pink"
                    }
                }
            };

            PokemonDefensiveCharacteristics secondItem = new()
            {
                No = new List<PokemonTypeDto>()
                {
                    new PokemonTypeDto()
                    {
                        Name = "Ghost",
                        Color = "Purple"
                    }
                },
                Quarter = new()
                {
                    new()
                    {
                        Name = "Grass",
                        Color = "Green"
                    }
                },
                Half = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Babyblue"
                    }
                },
                Neutral = new()
                {
                    new()
                    {
                        Name = "Normal",
                        Color = "Bronze"
                    }
                },
                Double = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                Quadruple = new()
                {
                    new()
                    {
                        Name = "Fairy",
                        Color = "Pink"
                    }
                }
            };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }
    }
}
