using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using PokemonAPI.DTOs;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class PokemonGeneralDtoTests
    {
        [TestMethod]
        public void FromPokemonAbilitiesAndTypes_MapsCorrectly()
        {
            // Arrange
            Pokemon pokemon = new Pokemon()
            {
                Name = "Bulbasaur",
                Sprite = "Bulbasaur.png",
                HP = 70,
                Attack = 80,
                Defense = 90,
                SpecialAttack = 100,
                SpecialDefense = 110,
                Speed = 120,
                PokemonEntries = new List<PokemonEntry>()
                {
                    new()
                    {
                        Pokedex = new()
                        {
                            Name = "National"
                        },
                        Number = 1,
                    }
                }
            };

            List<AbilityDto> abilities = new List<AbilityDto>();
            PokemonDefensiveCharacteristics defensiveCharacteristics = new PokemonDefensiveCharacteristics();
            PokemonTypeCharacteristics primaryCharacteristics = new PokemonTypeCharacteristics();
            PokemonTypeCharacteristics secondarayCharacteristics = new PokemonTypeCharacteristics();

            // Act
            var result = PokemonGeneralDto.FromPokemonAbilitiesAndTypes(pokemon, abilities, defensiveCharacteristics, primaryCharacteristics, secondarayCharacteristics);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pokemon.Name, result.Name);
            Assert.AreEqual(pokemon.Sprite, result.Image);
            Assert.AreEqual(pokemon.HP, result.HP);
            Assert.AreEqual(pokemon.Attack, result.Attack);
            Assert.AreEqual(pokemon.Defense, result.Defense);
            Assert.AreEqual(pokemon.SpecialAttack, result.SpecialAttack);
            Assert.AreEqual(pokemon.SpecialDefense, result.SpecialDefense);
            Assert.AreEqual(pokemon.Speed, result.Speed);
            Assert.AreEqual(pokemon.PokemonEntries.First().Number, result.Number);
            Assert.IsTrue(abilities == result.Abilities);
            Assert.IsTrue(defensiveCharacteristics == result.DefensiveRelations);
            Assert.IsTrue(primaryCharacteristics == result.PrimaryType);
            Assert.IsTrue(secondarayCharacteristics == result.SecondaryType);
        }

        [TestMethod]
        public void Equals_FalseIfNull()
        {
            // Assert
            PokemonGeneralDto firstItem = new PokemonGeneralDto();

            // Act
            var result = firstItem.Equals(null);

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_FalseIfOtherObject()
        {
            // Assert
            PokemonGeneralDto firstItem = new PokemonGeneralDto();

            // Act
            var result = firstItem.Equals(new List<int>());

            // Arrange
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_FalseIfSecondaryTypeNullAndOtherIsNot()
        {
            // Assert
            PokemonGeneralDto firstItem = new PokemonGeneralDto()
            {
                Name = "Bulbasaur",
                Image = "Bulbasaur.png",
                Number = 1,
                HP = 10,
                Attack = 20,
                Defense = 30,
                SpecialAttack = 40,
                SpecialDefense = 50,
                Speed = 60,
                Abilities = new List<AbilityDto>()
                {
                    new AbilityDto()
                    {
                        Name = "stinky",
                        Effect = "stinks"
                    }
                },
                DefensiveRelations = new()
                {
                    Half = new()
                    {
                        new()
                        {
                            Name = "Ice",
                            Color = "Icy"
                        }
                    }
                },
                PrimaryType = new()
                {
                    Name = "Grass",
                    Color = "Green"
                },
                SecondaryType = new()
                {
                    Name = "Poison",
                    Color = "Purple"
                }
            };

            PokemonGeneralDto secondItem = new PokemonGeneralDto()
            {
                Name = "Bulbasaur",
                Image = "Bulbasaur.png",
                Number = 1,
                HP = 10,
                Attack = 20,
                Defense = 30,
                SpecialAttack = 40,
                SpecialDefense = 50,
                Speed = 60,
                Abilities = new List<AbilityDto>()
                {
                    new AbilityDto()
                    {
                        Name = "stinky",
                        Effect = "stinks"
                    }
                },
                DefensiveRelations = new()
                {
                    Half = new()
                    {
                        new()
                        {
                            Name = "Ice",
                            Color = "Icy"
                        }
                    }
                },
                PrimaryType = new()
                {
                    Name = "Grass",
                    Color = "Green"
                },
                SecondaryType = null
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
            PokemonGeneralDto firstItem = new PokemonGeneralDto()
            {
                Name = "Bulbasaur",
                Image = "Bulbasaur.png",
                Number = 1,
                HP = 10,
                Attack = 20,
                Defense = 30,
                SpecialAttack = 40,
                SpecialDefense = 50,
                Speed = 60,
                Abilities = new List<AbilityDto>()
                {
                    new AbilityDto()
                    {
                        Name = "stinky",
                        Effect = "stinks"
                    }
                },
                DefensiveRelations = new()
                {
                    Half = new()
                    {
                        new()
                        {
                            Name = "Ice",
                            Color = "Icy"
                        }
                    }
                },
                PrimaryType = new()
                {
                    Name = "Grass",
                    Color = "Green"
                },
                SecondaryType = new()
                {
                    Name = "Poison",
                    Color = "Purple"
                }
            };

            PokemonGeneralDto secondItem = new PokemonGeneralDto()
            {
                Name = "Bulbasaur",
                Image = "Bulbasaur.png",
                Number = 1,
                HP = 10,
                Attack = 20,
                Defense = 30,
                SpecialAttack = 40,
                SpecialDefense = 50,
                Speed = 60,
                Abilities = new List<AbilityDto>()
                {
                    new AbilityDto()
                    {
                        Name = "stinky",
                        Effect = "stinks"
                    }
                },
                DefensiveRelations = new()
                {
                    Half = new()
                    {
                        new()
                        {
                            Name = "Ice",
                            Color = "Icy"
                        }
                    }
                },
                PrimaryType = new()
                {
                    Name = "Grass",
                    Color = "Green"
                },
                SecondaryType = new()
                {
                    Name = "Poison",
                    Color = "Purple"
                }
            };

            // Act
            var result = firstItem.Equals(secondItem);

            // Arrange
            Assert.IsTrue(result);
        }
    }
}
