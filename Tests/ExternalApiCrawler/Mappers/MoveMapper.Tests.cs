using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Generations;
using Models.Moves;
using Models.Types;
using Moq;
using System;
using System.Collections.Generic;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class MoveMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GenerationDto> _generationDtos;
        private List<MoveDto> _moveDtos;
        private List<Generation> _generations;
        private List<Move> _moves;
        private List<PokemonType> _pokemonTypes;
        private MoveMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _generationDtos = new List<GenerationDto>()
            {
                new GenerationDto()
                {
                    name = "favorite",
                    names = SingleEnglishNameGenerator.Generate("Favorite"),
                }
            };

            _generations = new List<Generation>()
            {
                new Generation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Favorite",
                }
            };

            _pokemonTypes = new List<PokemonType>()
            {
                new PokemonType
                {
                    Id = Guid.NewGuid(),
                    Name = "Sight"
                }
            };

            _moveDtos = new List<MoveDto>()
            {
                new MoveDto()
                {
                    name = "blink",
                    names = SingleEnglishNameGenerator.Generate("Blink"),
                    accuracy = 100,
                    pp = 15,
                    priority = 1,
                    power = 135,
                    effect_chance = 30,
                    damage_class = new Name()
                    {
                        name = "tons-of_damage"
                    },
                    generation = new Name()
                    {
                        name = "favorite"
                    },
                    effect_entries = new EffectEntry[]
                    {
                        new EffectEntry()
                        {
                            effect = "I do blinking. 30% to cause blindness",
                            language = new Name()
                            {
                                name = "en"
                            }
                        }
                    },
                    type = new Name()
                    {
                        name = "sight"
                    },
                    target = new Name()
                    {
                        name = "tri-state area"
                    }
                }
            };

            _moves = new List<Move>()
            {
                new Move()
                {
                    Name = "Blink",
                    Power = 135,
                    Accuracy = 100,
                    PP = 15,
                    Priority = 1,
                    TypeId = _pokemonTypes[0].Id,
                    Type = _pokemonTypes[0],
                    Category = "Tons Of Damage",
                    Effect = "I do blinking. 30% to cause blindness",
                    SpecialEffectChance = 30,
                    Target = "Tri State Area",
                    GenerationId = _generations[0].Id,
                    Generation = _generations[0]
                }
            };

            var gens = PokemonDatabaseContextMock.SetUpDbSetMock<Generation>(_generations);
            _databaseContext.Setup(dc => dc.Generations).Returns(gens.Object);
            var types = PokemonDatabaseContextMock.SetUpDbSetMock<PokemonType>(_pokemonTypes);
            _databaseContext.Setup(dc => dc.Types).Returns(types.Object);
            var moves = PokemonDatabaseContextMock.SetUpDbSetMock<Move>(new List<Move>());
            _databaseContext.Setup(dc => dc.Moves).Returns(moves.Object);

            _mapper = new MoveMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_moveDtos, _generationDtos);
        }

        [TestMethod]
        public void MapsCorrectly()
        {
            // Arrange
            var expectedMove = _moves[0];

            // Act
            var result = _mapper.Map();
            var actualMove = result[0];

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_moves.Count, result.Count);
            Assert.AreEqual(expectedMove.Name, actualMove.Name);
            Assert.AreEqual(expectedMove.Power, actualMove.Power);
            Assert.AreEqual(expectedMove.Accuracy, actualMove.Accuracy);
            Assert.AreEqual(expectedMove.PP, actualMove.PP);
            Assert.AreEqual(expectedMove.Priority, actualMove.Priority);
            Assert.AreEqual(expectedMove.TypeId, actualMove.TypeId);
            Assert.AreEqual(expectedMove.Category, actualMove.Category);
            Assert.AreEqual(expectedMove.Effect, actualMove.Effect);
            Assert.AreEqual(expectedMove.SpecialEffectChance, actualMove.SpecialEffectChance);
            Assert.AreEqual(expectedMove.Target, actualMove.Target);
            Assert.AreEqual(expectedMove.GenerationId, actualMove.GenerationId);
        }
    }
}
