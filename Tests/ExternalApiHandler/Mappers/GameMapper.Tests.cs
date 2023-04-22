using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Games;
using Models.Generations;
using Moq;
using System.Collections.Generic;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class GameMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private string _generationName;
        private List<GenerationDto> _generationDtos;
        private List<GamesDto> _gamesDtos;
        private List<Generation> _generations;
        private List<Game> _games;
        private GameMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _generationName = "favorite";
            _generationDtos = new List<GenerationDto>()
            {
                new GenerationDto()
                {
                    name = _generationName,
                    names = SingleEnglishNameWithLanguageGenerator.Generate("Favorite"),
                }
            };

            _generations = new List<Generation>()
            {
                new Generation()
                {
                    Name = "Favorite",
                }
            };

            _gamesDtos = new List<GamesDto>()
            {
                new GamesDto()
                {
                    VersionGroup = new VersionGroupDto
                    {
                        name = "version-group",
                        generation = new Name
                        {
                            name = _generationName
                        },
                    },
                    Versions = new List<VersionDto>()
                    {
                        new VersionDto
                        {
                            name = "first",
                            names = SingleEnglishNameWithLanguageGenerator.Generate("First"),
                        },
                        new VersionDto
                        {
                            name = "second",
                            names = SingleEnglishNameWithLanguageGenerator.Generate("Second")
                        }
                    }

                }
            };

            _games = new List<Game>()
            {
                new Game()
                {
                    Name = "First",
                    GenerationId = _generations[0].Id,
                    Generation = _generations[0],
                },
                new Game()
                {
                    Name = "Second",
                    GenerationId = _generations[0].Id,
                    Generation = _generations[0],
                }
            };

            var gens = PokemonDatabaseContextMock.SetUpDbSetMock<Generation>(_generations);
            _databaseContext.Setup(dc => dc.Generations).Returns(gens.Object);
            var games = PokemonDatabaseContextMock.SetUpDbSetMock<Game>(new List<Game>());
            _databaseContext.Setup(dc => dc.Games).Returns(games.Object);

            _mapper = new GameMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_gamesDtos, _generationDtos);
        }

        [TestMethod]
        public void MapsCorrectlyWithRegionsWhenThereIsOneRegion()
        {
            // Arrange
            Regions region = Regions.Johto;
            _gamesDtos[0].VersionGroup.regions = new Name[]
            {
                new Name
                {
                    name = region.ToString(),
                }
            };

            foreach(Game game in _games)
            {
                game.MainRegion = region;
            }

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_games.Count, result.Count);
            for(int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_games[i].MainRegion, result[i].MainRegion);
                Assert.AreEqual(_games[i].Name, result[i].Name);
                Assert.AreEqual(_generations[0], result[i].Generation);
            }
        }

        [TestMethod]
        public void MapsCorrectlyWithLastRegionIfThereIsMoreRegions()
        {
            // Arrange
            Regions region = Regions.Johto;
            _gamesDtos[0].VersionGroup.regions = new Name[]
            {
                 new Name
                {
                    name = Regions.Kanto.ToString(),
                },
                new Name
                {
                    name = region.ToString(),
                }
            };

            foreach (Game game in _games)
            {
                game.MainRegion = region;
            }

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_games.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_games[i].MainRegion, result[i].MainRegion);
                Assert.AreEqual(_games[i].Name, result[i].Name);
                Assert.AreEqual(_generations[0], result[i].Generation);
            }
        }

        [TestMethod]
        public void MapsCorrectlyWithEmptyRegionIfThereIsNoRegions()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_games.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsNull(result[i].MainRegion);
                Assert.AreEqual(_games[i].Name, result[i].Name);
                Assert.AreEqual(_generations[0], result[i].Generation);
            }
        }
    }
}
