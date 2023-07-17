using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Games;
using Models.Generations;
using Moq;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class GameVersionMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GamesDto> _gamesDtos;
        private List<Game> _games;
        private List<GameVersion> _gameVersions;
        private GameVersionMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _gamesDtos = new List<GamesDto>()
            {
                new GamesDto()
                {
                    VersionGroup = new VersionGroupDto
                    {
                        name = "version-group",
                        generation = new Name
                        {
                            name = "favorite"
                        },
                        pokedexes = new Name[0]
                    },
                    Versions = new List<VersionDto>()
                    {
                        new VersionDto
                        {
                            name = "first one",
                            names = SingleEnglishNameHelper.Generate("First One"),
                        },
                        new VersionDto
                        {
                            name = "second two",
                            names = SingleEnglishNameHelper.Generate("Second Two")
                        }
                    }

                }
            };

            Guid gameGuid = Guid.NewGuid();

            _games = new List<Game>()
            {
                new Game()
                {
                    Id = gameGuid,
                    Name = "Version Group",
                    PrettyName = "First One & Second Two",
                    GenerationId = Guid.NewGuid(),
                    Generation = new Models.Generations.Generation(),
                },
            };

            _gameVersions = new List<GameVersion>()
            {
                new GameVersion()
                {
                    Id = Guid.NewGuid(),
                    Name = "First One",
                    GameId = gameGuid,
                    Game = _games[0]
                },
                new GameVersion()
                {
                    Id = Guid.NewGuid(),
                    Name = "Second Two",
                    GameId = gameGuid,
                    Game = _games[0]
                },
            };

            var games = PokemonDbSetHelper.SetUpDbSetMock<Game>(_games);
            _databaseContext.Setup(dc => dc.Games).Returns(games.Object);
            var gameVersions = PokemonDbSetHelper.SetUpDbSetMock<GameVersion>(new List<GameVersion>());
            _databaseContext.Setup(dc => dc.GameVersions).Returns(gameVersions.Object);

            _mapper = new GameVersionMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_gamesDtos);
        }

        [TestMethod]
        public void MapsCorrectly_Simple()
        {
            // Arrange

            // Act
            var result = _mapper.MapToDb();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_gameVersions.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_gameVersions[i].Name, result[i].Name);
                Assert.AreEqual(_games[0].Id, result[i].GameId);
            }
        }
    }
}
