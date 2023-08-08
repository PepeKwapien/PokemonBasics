using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using Models.Types;
using Moq;
using PokemonAPI.Controllers;
using PokemonAPI.DTOs;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace Tests.PokemonAPI.Controllers
{
    [TestClass]
    public class PokemonControllerTests
    {
        private Mock<IPokemonService> _pokemonService;
        private Mock<IPokemonTypeService> _pokemonTypeService;
        private Mock<IAbilityService> _abilityService;
        private PokemonController _controller;

        private PokemonDefensiveCharacteristics _defensiveStats;
        private PokemonTypeCharacteristics _firstType;
        private PokemonTypeCharacteristics _secondType;

        [TestInitialize]
        public void Initialize()
        {
            _defensiveStats = new()
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

            _firstType = new()
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

            _secondType = new()
            {
                Name = "Dragon",
                Color = "Dark blue",
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

            _pokemonService = new Mock<IPokemonService>();
            _pokemonTypeService = new Mock<IPokemonTypeService>();
            _abilityService = new Mock<IAbilityService>();

            _controller = new PokemonController(_pokemonService.Object, _pokemonTypeService.Object, _abilityService.Object);
        }

        [TestMethod]
        public void GetGeneralInformation_ReturnsNotFoundIfPokemonNotFound()
        {
            // Arrange
            _pokemonService.Setup(ps => ps.GetPokemonByName(It.IsAny<string>())).Returns<Pokemon>(null);

            // Act
            var result = _controller.GetGeneralInformation("bulbasaur");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.GetType() == typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetGeneralInformation_ReturnsPokemonWithSingleType()
        {
            // Arrange
            Pokemon pokemon = new Pokemon()
            {
                Name = "Charmander",
                Sprite = "Charmander.png",
                HP = 30,
                Attack = 40,
                Defense = 50,
                SpecialAttack = 60,
                SpecialDefense = 70,
                Speed = 80,
                PokemonEntries = new List<PokemonEntry>()
                    {
                        new()
                        {
                            Number = 4,
                            Pokedex = new()
                            {
                                Name = "National"
                            }
                        }
                    },
                PrimaryType = new PokemonType()
                {
                    Name = "Fire"
                }
            };

            _pokemonService.Setup(ps => ps.GetPokemonByName(It.IsAny<string>())).Returns(pokemon);

            List<AbilityDto> abilities = new List<AbilityDto>()
            {
                new AbilityDto()
                {
                    Name = "Blaze",
                    Effect = "Blazes"
                }
            };

            _abilityService.Setup(abs => abs.GetAbilitiesDtoForPokemon("Charmander")).Returns(abilities);

            _pokemonTypeService.Setup(pts => pts.GetDefensiveCharacteristics("Fire", null)).Returns(_defensiveStats);
            _pokemonTypeService.Setup(pts => pts.GetTypeCharacteristic("Fire")).Returns(_firstType);

            PokemonGeneralDto expected = new()
            {
                Name = pokemon.Name,
                Image = pokemon.Sprite,
                HP = pokemon.HP,
                Attack= pokemon.Attack,
                SpecialAttack= pokemon.SpecialAttack,
                Defense = pokemon.Defense,
                SpecialDefense = pokemon.SpecialDefense,
                Speed = pokemon.Speed,
                Number = pokemon.PokemonEntries.First().Number,
                Abilities = abilities,
                DefensiveRelations = _defensiveStats,
                PrimaryType = _firstType,
                SecondaryType = null
            };

            // Act
            var result = _controller.GetGeneralInformation("charmander");
            var resultOkOjbect = result as OkObjectResult;
            var resultValue = resultOkOjbect?.Value as PokemonGeneralDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultOkOjbect);
            Assert.IsNotNull(resultValue);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
            Assert.AreEqual(expected, resultValue);
        }

        [TestMethod]
        public void GetGeneralInformation_ReturnsPokemonWithDoubleType()
        {
            // Arrange
            Pokemon pokemon = new Pokemon()
            {
                Name = "Bulbasaur",
                Sprite = "Bulbasaur.png",
                HP = 30,
                Attack = 40,
                Defense = 50,
                SpecialAttack = 60,
                SpecialDefense = 70,
                Speed = 80,
                PokemonEntries = new List<PokemonEntry>()
                    {
                        new()
                        {
                            Number = 4,
                            Pokedex = new()
                            {
                                Name = "National"
                            }
                        }
                    },
                PrimaryType = new PokemonType()
                {
                    Name = "Grass"
                },
                SecondaryType = new PokemonType()
                {
                    Name = "Poison"
                }
            };

            _pokemonService.Setup(ps => ps.GetPokemonByName(It.IsAny<string>())).Returns(pokemon);

            List<AbilityDto> abilities = new List<AbilityDto>()
            {
                new AbilityDto()
                {
                    Name = "Overgrow",
                    Effect = "Overgrows"
                }
            };

            _abilityService.Setup(abs => abs.GetAbilitiesDtoForPokemon("Bulbasaur")).Returns(abilities);

            _pokemonTypeService.Setup(pts => pts.GetDefensiveCharacteristics("Grass", "Poison")).Returns(_defensiveStats);
            _pokemonTypeService.Setup(pts => pts.GetTypeCharacteristic("Grass")).Returns(_firstType);
            _pokemonTypeService.Setup(pts => pts.GetTypeCharacteristic("Poison")).Returns(_secondType);

            PokemonGeneralDto expected = new()
            {
                Name = pokemon.Name,
                Image = pokemon.Sprite,
                HP = pokemon.HP,
                Attack = pokemon.Attack,
                SpecialAttack = pokemon.SpecialAttack,
                Defense = pokemon.Defense,
                SpecialDefense = pokemon.SpecialDefense,
                Speed = pokemon.Speed,
                Number = pokemon.PokemonEntries.First().Number,
                Abilities = abilities,
                DefensiveRelations = _defensiveStats,
                PrimaryType = _firstType,
                SecondaryType = _secondType
            };

            // Act
            var result = _controller.GetGeneralInformation("bulbasaur");
            var resultOkOjbect = result as OkObjectResult;
            var resultValue = resultOkOjbect?.Value as PokemonGeneralDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultOkOjbect);
            Assert.IsNotNull(resultValue);
            Assert.IsTrue(result.GetType() == typeof(OkObjectResult));
            Assert.AreEqual(expected, resultValue);
        }
    }
}
