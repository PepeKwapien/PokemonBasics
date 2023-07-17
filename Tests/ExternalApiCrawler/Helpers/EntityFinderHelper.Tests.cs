using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Tests.ExternalApiCrawler.Helpers
{
    [TestClass]
    public class EntityFinderHelperTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GamesDto> _gamesDtos;
        private List<Game> _games;
        private List<GenerationDto> _generationDtos;
        private List<Generation> _generations;

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
            _generationDtos = new List<GenerationDto>()
            {
                new GenerationDto
                {
                    name = "first",
                    names = SingleEnglishNameHelper.Generate("1")
                },
                new GenerationDto
                {
                    name = "second",
                    names = SingleEnglishNameHelper.Generate("2")
                },
                new GenerationDto
                {
                    name = "third",
                    names = SingleEnglishNameHelper.Generate("3")
                },
            };
            _generations = new List<Generation>()
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
        public void FindTypeByNameCaseInsensitive_ReturnsExceptionWhenNameNotExisting()
        {
            // Arrange
            string notExistingType = "i dont exist";
            var typeSet = PokemonDbSetHelper.SetUpDbSetMock(new List<PokemonType>());
            _databaseContext.Setup(dbc => dbc.Types).Returns(typeSet.Object);

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => EntityFinderHelper.FindTypeByNameCaseInsensitive(_databaseContext.Object.Types, notExistingType));
            Assert.AreEqual($"Type {notExistingType} does not exist in the database", exception.Message);
        }

        [TestMethod]
        public void FindEntityByDtoName_FindsCorrectGeneration()
        {
            // Arrange
            var generationSet = PokemonDbSetHelper.SetUpDbSetMock(_generations);
            _databaseContext.Setup(dbc => dbc.Generations).Returns(generationSet.Object);

            // Act
            var result = EntityFinderHelper.FindEntityByDtoName(_databaseContext.Object.Generations, "second", _generationDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("2", result.Name);
        }

        [TestMethod]
        public void FindEntityByDtoName_ThrowsErrorWhenNoMatchingDto()
        {
            // Arrange
            string dtoName = "forth";
            var generationSet = PokemonDbSetHelper.SetUpDbSetMock(_generations);
            _databaseContext.Setup(dbc => dbc.Generations).Returns(generationSet.Object);

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => EntityFinderHelper.FindEntityByDtoName(_databaseContext.Object.Generations, dtoName, _generationDtos));
            Assert.AreEqual($"No dto of type {typeof(GenerationDto).Name} was found to match name {dtoName}", exception.Message);
        }

        [TestMethod]
        public void FindEntityByDtoName_ThrowsErrorWhenNoEntityInDb()
        {
            // Arrange
            var generationSet = PokemonDbSetHelper.SetUpDbSetMock(_generations);
            _databaseContext.Setup(dbc => dbc.Generations).Returns(generationSet.Object);

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => EntityFinderHelper.FindEntityByDtoName(_databaseContext.Object.Generations, "third", _generationDtos));
            Assert.AreEqual($"No entity of type {typeof(Generation).Name} was found to match name 3", exception.Message);
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
        public void FindPokemonByName_ThrowsIfPokemonDoesNotExist()
        {
            // Arrange
            string pokemonName = "dani-devito";
            var pokemonSet = PokemonDbSetHelper.SetUpDbSetMock(new List<Pokemon>());
            _databaseContext.Setup(dbc => dbc.Pokemons).Returns(pokemonSet.Object);

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => EntityFinderHelper.FindPokemonByName(_databaseContext.Object.Pokemons, pokemonName));
            Assert.AreEqual($"Pokemon with name {StringHelper.Normalize(pokemonName)} does not exist in the database", exception.Message);
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
        public void FindGamesByVersionGroupName_ThrowsIfVersionGroupDoesnotExist()
        {
            // Arrange
            string versionGroupName = "notexisting";
            var gameSet = PokemonDbSetHelper.SetUpDbSetMock(_games);
            _databaseContext.Setup(dbc => dbc.Games).Returns(gameSet.Object);

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() => EntityFinderHelper.FindGamesByVersionGroupName(_databaseContext.Object.Games, versionGroupName, _gamesDtos));
            Assert.AreEqual($"No game dto was found to match version group name {versionGroupName}", exception.Message);
        }

        [TestMethod]
        public void FindVarietiesInPokemonSpecies_FindsAllVarieties()
        {
            // Arrange
            List<Pokemon> pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Bulbasaur Lovely"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Bulbasaur Wobbly"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Bulbasaur Godly"
                }
            };

            var pokemonSet = PokemonDbSetHelper.SetUpDbSetMock(pokemons);
            _databaseContext.Setup(dbc => dbc.Pokemons).Returns(pokemonSet.Object);

            List<PokemonSpeciesDto> pokemonSpeciesDtos = new List<PokemonSpeciesDto>()
            {
                new PokemonSpeciesDto
                {
                    name = "bulbasaur",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default= true,
                            pokemon = new Name
                            {
                                name = "bulbasaur-lovely"
                            }
                        },
                        new Variety
                        {
                            is_default= true,
                            pokemon = new Name
                            {
                                name = "bulbasaur-wobbly"
                            }
                        },
                        new Variety
                        {
                            is_default= true,
                            pokemon = new Name
                            {
                                name = "bulbasaur-godly"
                            }
                        }
                    }
                }
            };

            // Act
            var result = EntityFinderHelper.FindRegionalFormsInSpecies(_databaseContext.Object.Pokemons, "bulbasaur", pokemonSpeciesDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pokemons.Count, result.Count);
            for(int i = 0; i < pokemons.Count; i++)
            {
                Assert.AreEqual(pokemons[i], result[i]);
            }
        }

        [TestMethod]
        public void FindVarietiesInPokemonSpecies_ThrowsIfSpeciesNotFound()
        {
            // Arrange
            string speciesName = "bulbasaur";

            // Act

            // Assert
            var exception = Assert.ThrowsException<Exception>(() =>
                EntityFinderHelper.FindRegionalFormsInSpecies(_databaseContext.Object.Pokemons, speciesName, new List<PokemonSpeciesDto>()));
            Assert.AreEqual($"No matching pokemon species was found under the name {speciesName}", exception.Message);
        }
    }
}
