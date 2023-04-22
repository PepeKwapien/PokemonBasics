using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Models.Games;
using Models.Generations;
using Moq;
using System;
using System.Collections.Generic;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class AbilityMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GenerationDto> _generationDtos;
        private List<AbilityDto> _abilityDtos;
        private List<Generation> _generations;
        private List<Ability> _abilities;
        private AbilityMapper _mapper;

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
                    names = SingleEnglishNameWithLanguageGenerator.Generate("Favorite"),
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

            _abilityDtos = new List<AbilityDto>
            {
                new AbilityDto()
                {
                    name = "stinky",
                    names = SingleEnglishNameWithLanguageGenerator.Generate("Stinky"),
                    is_main_series = true,
                    effect_entries = new EffectEntry[]
                    {
                        new EffectEntry()
                        {
                            effect = "It stinks",
                            language = new Name
                            {
                                name = "en"
                            }
                        }
                    },
                    generation = new Name
                    {
                        name = "favorite"
                    }
                }
            };

            _abilities = new List<Ability>()
            {
                new Ability()
                {
                    Name = "Stinky",
                    Effect = "It stinks",
                    Generation = _generations[0],
                    GenerationId = _generations[0].Id
                }
            };

            var gens = PokemonDatabaseContextMock.SetUpDbSetMock<Generation>(_generations);
            _databaseContext.Setup(dc => dc.Generations).Returns(gens.Object);
            var abilities = PokemonDatabaseContextMock.SetUpDbSetMock<Ability>(new List<Ability>());
            _databaseContext.Setup(dc => dc.Abilities).Returns(abilities.Object);

            _mapper = new AbilityMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_abilityDtos, _generationDtos);
        }

        [TestMethod]
        public void MapsCorrectlyBasicFields()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_abilities.Count, result.Count);
            Assert.AreEqual(_abilities[0].Name, result[0].Name);
            Assert.AreEqual(_abilities[0].Effect, result[0].Effect);
            Assert.IsNull(result[0].OverworldEffect);
            Assert.AreEqual(_abilities[0].GenerationId, result[0].GenerationId);
        }

        [TestMethod]
        public void MapsCorrectlyOverWorldEffect()
        {
            // Arrange
            string effect = _abilityDtos[0].effect_entries[0].effect;
            string overworldEffect = "Really badly";
            _abilityDtos[0].effect_entries[0].effect = $"{effect} Overworld:\t{overworldEffect}";

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_abilities.Count, result.Count);
            Assert.AreEqual(_abilities[0].Name, result[0].Name);
            Assert.AreEqual(effect, result[0].Effect);
            Assert.IsNotNull(result[0].OverworldEffect);
            Assert.AreEqual(overworldEffect, result[0].OverworldEffect);
            Assert.AreEqual(_abilities[0].GenerationId, result[0].GenerationId);
        }

        [TestMethod]
        public void MapsCorrectlyAndLeavesEffectEmptyWhenNoEffectEntries()
        {
            // Arrange
            _abilityDtos[0].effect_entries = new EffectEntry[0]; 

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_abilities.Count, result.Count);
            Assert.AreEqual(_abilities[0].Name, result[0].Name);
            Assert.AreEqual("", result[0].Effect);
            Assert.IsNull(result[0].OverworldEffect);
            Assert.AreEqual(_abilities[0].GenerationId, result[0].GenerationId);
        }
    }
}
