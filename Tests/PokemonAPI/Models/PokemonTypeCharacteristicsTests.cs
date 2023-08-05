using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests.PokemonAPI.Models
{
    [TestClass]
    public class PokemonTypeCharacteristicsTests
    {
        [TestMethod]
        public void FromPokemonTypeAndDamageMultipliers_MapsCorrectly()
        {
            // Arrange
            PokemonType type = new()
            {
                Name = "Grass",
                Color = "Green"
            };

            PokemonType water = new PokemonType()
            {
                Name = "Water",
                Color = "Blue"
            };
            PokemonType fire = new PokemonType()
            {
                Name = "Fire",
                Color = "Red"
            };
            PokemonType electric = new PokemonType()
            {
                Name = "Electric",
                Color = "Yellow"
            };
            PokemonType flying = new PokemonType()
            {
                Name = "Flying",
                Color = "Cyan"
            };
            PokemonType normal = new PokemonType()
            {
                Name = "Normal",
                Color = "Beige"
            };
            PokemonType fighting = new PokemonType()
            {
                Name = "Fighting",
                Color = "Maroon"
            };

            List<DamageMultiplier> multipliers = new List<DamageMultiplier>()
            {
                new()
                {
                    Type = type,
                    Multiplier = 0,
                    Against = fighting
                },
                new()
                {
                    Type = normal,
                    Multiplier = 0,
                    Against = type
                },
                new()
                {
                    Type = type,
                    Multiplier = 0.5,
                    Against = fire
                },
                new()
                {
                    Type = water,
                    Multiplier = .5,
                    Against = type
                },
                new()
                {
                    Type = type,
                    Multiplier = 2,
                    Against = electric
                },
                new()
                {
                    Type = flying,
                    Multiplier = 2,
                    Against = type
                }
            };

            // Act
            var result = PokemonTypeCharacteristics.FromPokemonTypeAndDamageMultipliers(type, multipliers);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(type.Name, result.Name);
            Assert.AreEqual(type.Color, result.Color);
            Assert.AreEqual(fighting.Name, result.NoTo.First().Name);
            Assert.AreEqual(normal.Name, result.NoFrom.First().Name);
            Assert.AreEqual(fire.Name, result.HalfTo.First().Name);
            Assert.AreEqual(water.Name, result.HalfFrom.First().Name);
            Assert.AreEqual(electric.Name, result.DoubleTo.First().Name);
            Assert.AreEqual(flying.Name, result.DoubleFrom.First().Name);
        }

        [TestMethod]
        public void Equals_TrueWhenIdentical()
        {
            // Arrange
            PokemonTypeCharacteristics first = new()
            {
                Name = "Grass",
                Color = "Green",
                NoTo = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                NoFrom = new(),
                HalfTo = new(),
                HalfFrom = new()
                {
                    new()
                    {
                        Name = "Water",
                        Color = "Blue"
                    },
                    new()
                    {
                        Name = "Ground",
                        Color = "Bronze"
                    }
                },
                DoubleTo = new()
                {
                    new()
                    {
                        Name = "Rock",
                        Color = "Bronze"
                    }
                },
                DoubleFrom = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Lightblue"
                    }
                }
            };

            PokemonTypeCharacteristics second = new()
            {
                Name = "Grass",
                Color = "Green",
                NoTo = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                NoFrom = new(),
                HalfTo = new(),
                HalfFrom = new()
                {
                    new()
                    {
                        Name = "Water",
                        Color = "Blue"
                    },
                    new()
                    {
                        Name = "Ground",
                        Color = "Bronze"
                    }
                },
                DoubleTo = new()
                {
                    new()
                    {
                        Name = "Rock",
                        Color = "Bronze"
                    }
                },
                DoubleFrom = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Lightblue"
                    }
                }
            };

            // Act
            bool result = first.Equals(second);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_FalseWhenOneCollectionDiffers()
        {
            // Arrange
            PokemonTypeCharacteristics first = new()
            {
                Name = "Grass",
                Color = "Green",
                NoTo = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                NoFrom = new(),
                HalfTo = new(),
                HalfFrom = new()
                {
                    new()
                    {
                        Name = "Water",
                        Color = "Blue"
                    },
                    new()
                    {
                        Name = "Ground",
                        Color = "Bronze"
                    }
                },
                DoubleTo = new()
                {
                    new()
                    {
                        Name = "Rock",
                        Color = "Bronze"
                    }
                },
                DoubleFrom = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Lightblue"
                    }
                }
            };

            PokemonTypeCharacteristics second = new()
            {
                Name = "Grass",
                Color = "Green",
                NoTo = new()
                {
                    new()
                    {
                        Name = "Fire",
                        Color = "Red"
                    }
                },
                NoFrom = new()
                {
                    new()
                    {
                        Name = "Ghost",
                        Color = "Purple"
                    }
                },
                HalfTo = new(),
                HalfFrom = new()
                {
                    new()
                    {
                        Name = "Water",
                        Color = "Blue"
                    },
                    new()
                    {
                        Name = "Ground",
                        Color = "Bronze"
                    }
                },
                DoubleTo = new()
                {
                    new()
                    {
                        Name = "Rock",
                        Color = "Bronze"
                    }
                },
                DoubleFrom = new()
                {
                    new()
                    {
                        Name = "Ice",
                        Color = "Lightblue"
                    }
                }
            };

            // Act
            bool result = first.Equals(second);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
