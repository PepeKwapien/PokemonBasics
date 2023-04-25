using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Models.Games;
using Models.Moves;
using Models.Pokemons;
using Moq;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class PokemonMoveMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonDto> _pokemonDtos;
        private List<MoveDto> _moveDtos;
        private List<GamesDto> _gamesDtos;
        private List<Pokemon> _pokemons;
        private List<Move> _moves;
        private List<Game> _games;
        private PokemonMoveMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _pokemonDtos= new List<PokemonDto>()
            {
                new PokemonDto()
                {
                    name = "bulbasaur",
                    moves = new InnerPokemonMove[]
                    {
                        new InnerPokemonMove()
                        {
                            move = new Name
                            {
                                name = "razor-leaf"
                            },
                            version_group_details = new VersionGroupDetails[]
                            {
                                new VersionGroupDetails
                                {
                                    level_learned_at = 16,
                                    move_learned_method = new Name
                                    {
                                        name = "tutor"
                                    },
                                    version_group = new Name
                                    {
                                        name = "red-blue"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            _pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Bulbasaur"
                }
            };

            _moveDtos = new List<MoveDto>()
            {
                new MoveDto()
                {
                    name = "razor-leaf",
                    names = SingleEnglishNameHelper.Generate("Razor Leaf")
                }
            };

            _moves = new List<Move>()
            {
                new Move()
                {
                    Id = Guid.NewGuid() ,
                    Name = "Razor Leaf"
                }
            };

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
            };

            var poks = PokemonDbSetHelper.SetUpDbSetMock<Pokemon>(_pokemons);
            _databaseContext.Setup(dc => dc.Pokemons).Returns(poks.Object);
            var moves = PokemonDbSetHelper.SetUpDbSetMock<Move>(_moves);
            _databaseContext.Setup(dc => dc.Moves).Returns(moves.Object);
            var games = PokemonDbSetHelper.SetUpDbSetMock<Game>(_games);
            _databaseContext.Setup(dc => dc.Games).Returns(games.Object);
            var pokemonMoves = PokemonDbSetHelper.SetUpDbSetMock<PokemonMove>(new List<PokemonMove>());
            _databaseContext.Setup(dc => dc.PokemonMoves).Returns(pokemonMoves.Object);

            _mapper = new PokemonMoveMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_pokemonDtos, _moveDtos, _gamesDtos);
        }

        [TestMethod]
        public void MapsCorrectly_Simple()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_games.Count, result.Count);
            for(int i = 0; i < _games.Count; i++)
            {
                Assert.AreEqual(_pokemons[0].Id, result[i].PokemonId);
                Assert.AreEqual(_moves[0].Id, result[i].MoveId);
                Assert.AreEqual(_games[i].Id, result[i].GameId);
            }
        }
    }
}
