﻿using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Games;
using Models.Generations;
using Models.Pokeballs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class PokeballMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GenerationDto> _generationDtos;
        private List<PokeballDto> _pokeballDtos;
        private List<Generation> _generations;
        private List<Pokeball> _pokeballs;
        private PokeballMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext= new Mock<IPokemonDatabaseContext>();

            _generationDtos = new List<GenerationDto>()
            {
                new GenerationDto()
                {
                    name = "first",
                    names = SingleEnglishNameGenerator.Generate("1"),
                },
                new GenerationDto()
                {
                    name = "second",
                    names = SingleEnglishNameGenerator.Generate("2"),
                },
                new GenerationDto()
                {
                    name = "third",
                    names = SingleEnglishNameGenerator.Generate("3"),
                }
            };

            _generations = new List<Generation>()
            {
                new Generation()
                {
                    Id = Guid.NewGuid(),
                    Name = "1",
                },
                new Generation()
                {
                    Id = Guid.NewGuid(),
                    Name = "2",
                },
                new Generation()
                {
                    Id = Guid.NewGuid(),
                    Name = "3",
                }
            };

            string sharedName = "Pokeball";
            string sharedDescription = "Does something amazing";

            _pokeballDtos = new List<PokeballDto>()
            {
                new PokeballDto()
                {
                    name = "pokeball",
                    names = SingleEnglishNameGenerator.Generate(sharedName),
                    effect_entries = new EffectEntry[]
                    {
                        new EffectEntry()
                        {
                            effect = sharedDescription,
                            language = new Name
                            {
                                name = "en",
                            }
                        }
                    },
                    game_indices = new GameIndice[0]
                }
            };

            _pokeballs = new List<Pokeball>()
            {
                new Pokeball()
                {
                    Name = sharedName,
                    Description = sharedDescription,
                    Generations = new List<Generation>()
                }
            };

            var gens = PokemonDatabaseContextMock.SetUpDbSetMock<Generation>(_generations);
            _databaseContext.Setup(dc => dc.Generations).Returns(gens.Object);
            var pokeballs = PokemonDatabaseContextMock.SetUpDbSetMock<Pokeball>(new List<Pokeball>());
            _databaseContext.Setup(dc => dc.Pokeballs).Returns(pokeballs.Object);

            _mapper = new PokeballMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_pokeballDtos, _generationDtos);
        }

        [TestMethod]
        public void MapsCorrectlyFields()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokeballs.Count, result.Count);
            Assert.AreEqual(_pokeballs[0].Name, result[0].Name);
            Assert.AreEqual(_pokeballs[0].Description, result[0].Description);
            Assert.AreEqual(0, result[0].Generations.Count);
        }

        [TestMethod]
        public void LinksCorrectlyGenerations()
        {
            // Arrange
            _pokeballDtos[0].game_indices = _generationDtos.Select(generation => new GameIndice()
            {
                generation = new Name
                {
                    name = generation.name
                }
            }).ToArray();

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokeballs.Count, result.Count);
            Assert.AreEqual(_generations.Count, result[0].Generations.Count);
            for (int i = 0; i < _generations.Count; i++)
            {
                Assert.AreEqual(_generations[i].Id, result[0].Generations.ToList()[i].Id);
            }
        }
    }
}