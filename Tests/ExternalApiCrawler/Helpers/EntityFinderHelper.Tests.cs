using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Games;
using Models.Generations;
using Models.Pokemons;
using Models.Types;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tests.TestHelpers;
using Tests.TestHelpers;

namespace Tests.ExternalApiHandler.Helpers
{
    [TestClass]
    public class EntityFinderHelperTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GamesDto> _gamesDtos;
        private List<Game> _games;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext= new Mock<IPokemonDatabaseContext>();
            _gamesDtos = new List<GamesDto>()
            {
                new GamesDto
                {
                    VersionGroup = new VersionGroupDto()
                    {
                        name = "red-blue"
                    },
                    Versions = new List<VersionDto>()
                    {
                        new VersionDto
                        {
                            name = "red",
                            names = SingleEnglishNameHelper.Generate("Red")
                        },
                        new VersionDto
                        {
                            name = "blue",
                            names = SingleEnglishNameHelper.Generate("Blue")
                        },
                    }
                },
                new GamesDto
                {
                    VersionGroup = new VersionGroupDto()
                    {
                        name = "legends"
                    },
                    Versions = new List<VersionDto>()
                    {
                        new VersionDto
                        {
                            name = "arceus",
                            names = SingleEnglishNameHelper.Generate("Arceus")
                        },
                    }
                }
            };
            _games = new List<Game>()
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Red"
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Blue"
                },
                new Game
                {
                    Id = Guid.NewGuid(),
                    Name = "Arceus"
                },
            };
        }

        [TestMethod]
        public void FindTypeByNameCaseInsensitive_FindsCorrectTypeWhenNameMatching()
        {
            // Arrange
            string[] typeNames = new string[] { "Ice, Grass, Water, Fire, Electric, Ground" };
            List<PokemonType> types = typeNames.ToList().Select(typeName => new PokemonType { Name = typeName }).ToList();

            var typeSet = PokemonDbSetHelper.SetUpDbSetMock(types);
            _databaseContext.Setup(dbc => dbc.Types).Returns(typeSet.Object);

            int randomIndex = new Random().Next(typeNames.Length);

            // Act
            var foundType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_databaseContext.Object.Types, typeNames[randomIndex]);

            // Assert
            Assert.IsNotNull(foundType);
            Assert.AreEqual(types[randomIndex].Name, foundType.Name);
        }

        [TestMethod]
        public void FindTypeByNameCaseInsensitive_FindsCorrectTypeWithLowercaseName()
        {
            // Arrange
            string[] typeNames = new string[] { "ice, grass, water, fire, electric, ground" };
            List<PokemonType> types = typeNames.ToList().Select(typeName => new PokemonType
            {
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(typeName)
            })
                .ToList();

            var typeSet = PokemonDbSetHelper.SetUpDbSetMock(types);
            _databaseContext.Setup(dbc => dbc.Types).Returns(typeSet.Object);

            int randomIndex = new Random().Next(typeNames.Length);

            // Act
            var foundType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_databaseContext.Object.Types, typeNames[randomIndex]);

            // Assert
            Assert.IsNotNull(foundType);
            Assert.AreEqual(types[randomIndex].Name, foundType.Name);
        }

        [TestMethod]
        public void FindEntityByDtoName_FindsCorrectGeneration()
        {
            // Arrange
            string nameToSearch = "second";
            string englishNameToFind = "2";

            List<GenerationDto> generationDtos = new List<GenerationDto>()
            {
                new GenerationDto
                {
                    name = "first",
                    names = SingleEnglishNameHelper.Generate("1")
                },
                new GenerationDto
                {
                    name = nameToSearch,
                    names = SingleEnglishNameHelper.Generate(englishNameToFind)
                },
                new GenerationDto
                {
                    name = "third",
                    names = SingleEnglishNameHelper.Generate("3")
                },
            };

            List<Generation> generations = new List<Generation>()
            {
                new Generation
                {
                    Name = "1",
                    Region = Regions.Kanto
                },
                new Generation
                {
                    Name = "2",
                    Region = Regions.Johto
                },
                new Generation
                {
                    Name = "3",
                    Region = Regions.Hoenn
                }
            };

            var generationSet = PokemonDbSetHelper.SetUpDbSetMock(generations);
            _databaseContext.Setup(dbc => dbc.Generations).Returns(generationSet.Object);

            // Act
            var result = EntityFinderHelper.FindEntityByDtoName(_databaseContext.Object.Generations, nameToSearch, generationDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(englishNameToFind, result.Name);
        }

        [TestMethod]
        public void FindPokemonByName_FindsCorrectPokemon()
        {
            // Arrange
            List<Pokemon> pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Name = "Dani Devito"
                },
                new Pokemon
                {
                    Name = "Amy Kay"
                }
            };

            var pokemonSet = PokemonDbSetHelper.SetUpDbSetMock(pokemons);
            _databaseContext.Setup(dbc => dbc.Pokemons).Returns(pokemonSet.Object);

            // Act
            var result1 = EntityFinderHelper.FindPokemonByName(_databaseContext.Object.Pokemons, "dani-devito");
            var result2 = EntityFinderHelper.FindPokemonByName(_databaseContext.Object.Pokemons, "amy_kay");

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual(pokemons[0], result1);
            Assert.AreEqual(pokemons[1], result2);
        }

        [TestMethod]
        public void FindGamesByVersionGroupName_FindsCorrectSingleGame()
        {
            // Arrange
            var gameSet = PokemonDbSetHelper.SetUpDbSetMock(_games);
            _databaseContext.Setup(dbc => dbc.Games).Returns(gameSet.Object);

            // Act
            var result = EntityFinderHelper.FindGamesByVersionGroupName(_databaseContext.Object.Games, "legends", _gamesDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_games[2].Id, result[0].Id);
        }

        [TestMethod]
        public void FindGamesByVersionGroupName_FindsMultipleGames()
        {
            // Arrange
            var gameSet = PokemonDbSetHelper.SetUpDbSetMock(_games);
            _databaseContext.Setup(dbc => dbc.Games).Returns(gameSet.Object);

            // Act
            var result = EntityFinderHelper.FindGamesByVersionGroupName(_databaseContext.Object.Games, "red-blue", _gamesDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            for(int i = 0; i < 2; i++)
            {
                Assert.AreEqual(_games[i].Id, result[i].Id);
            }
        }

        [TestMethod]
        public void FindGamesByVersionGroupName_ReturnsEmptyListIfVersionGroupDoesnotExist()
        {
            // Arrange
            var gameSet = PokemonDbSetHelper.SetUpDbSetMock(_games);
            _databaseContext.Setup(dbc => dbc.Games).Returns(gameSet.Object);

            // Act
            var result = EntityFinderHelper.FindGamesByVersionGroupName(_databaseContext.Object.Games, "notexisting", _gamesDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
