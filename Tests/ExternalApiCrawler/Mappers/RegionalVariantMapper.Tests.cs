using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokemons;
using Moq;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class RegionalVariantMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<Pokemon> _pokemons;
        private RegionalVariantMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();
            _pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Bulbasaur"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth Alola"
                },
            };

            var poks = PokemonDbSetHelper.SetUpDbSetMock<Pokemon>(_pokemons);
            _databaseContext.Setup(dc => dc.Pokemons).Returns(poks.Object);
            var regVar = PokemonDbSetHelper.SetUpDbSetMock<RegionalVariant>(new List<RegionalVariant>());
            _databaseContext.Setup(dc => dc.RegionalVariants).Returns(regVar.Object);

            _mapper = new RegionalVariantMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void CreatesNoAlternateFormsIfNoVarieties()
        {
            // Arrange
            List<PokemonSpeciesDto> speciesDtos = new List<PokemonSpeciesDto>()
            {
                new PokemonSpeciesDto
                {
                    name = "bulbasaur",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "bulbasaur"
                            }
                        }
                    }
                }
            };

            _mapper.SetUp(speciesDtos);

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void MapsAllAlternateForms()
        {
            // Arrange
            List<PokemonSpeciesDto> speciesDtos = new List<PokemonSpeciesDto>()
            {
                new PokemonSpeciesDto
                {
                    name = "meowth",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "meowth"
                            }
                        },
                        new Variety
                        {
                            is_default = false,
                            pokemon = new Name
                            {
                                name = "meowth-galar"
                            }
                        },
                        new Variety
                        {
                            is_default = false,
                            pokemon = new Name
                            {
                                name = "meowth-alola"
                            }
                        },
                    }
                }
            };

            _mapper.SetUp(speciesDtos);

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(speciesDtos[0].varieties.Length - 1, result.Count);

            for(int i = 0; i < speciesDtos[0].varieties.Length - 1; i++)
            {
                Assert.AreEqual(_pokemons[1].Id, result[i].OriginalId);
                Assert.AreEqual(_pokemons[2 + i].Id, result[i].VariantId);
            }
        }
    }
}
