using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Models.Generations;
using Models.Pokemons;
using Moq;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class PokemonAbilityMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonDto> _pokemonDtos;
        private List<AbilityDto> _abilityDtos;
        private List<Pokemon> _pokemons;
        private List<Ability> _abilities;
        private PokemonAbilityMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _pokemonDtos = new List<PokemonDto>()
            {
                new PokemonDto
                {
                    name = "first",
                    abilities = new PokemonAbilityDto[]
                    {
                        new PokemonAbilityDto
                        {
                            slot = 1,
                            is_hidden = false,
                            ability = new Name
                            {
                                name = "veni"
                            }
                        },
                        new PokemonAbilityDto
                        {
                            slot = 2,
                            is_hidden = false,
                            ability = new Name
                            {
                                name = "vidi"
                            }
                        },
                        new PokemonAbilityDto
                        {
                            slot = 3,
                            is_hidden = true,
                            ability = new Name
                            {
                                name = "vici"
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
                    Name = "First"
                }
            };

            _abilityDtos = new List<AbilityDto>()
            {
                new AbilityDto
                {
                    name = "veni",
                    names = SingleEnglishNameHelper.Generate("Veni"),
                },
                new AbilityDto
                {
                    name = "vidi",
                    names = SingleEnglishNameHelper.Generate("Vidi"),
                },
                new AbilityDto
                {
                    name = "vici",
                    names = SingleEnglishNameHelper.Generate("Vici"),
                },
            };

            _abilities = new List<Ability>()
            {
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Veni"
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Vidi"
                },
                new Ability
                {
                    Id = Guid.NewGuid(),
                    Name = "Vici"
                },
            };

            var poks = PokemonDbSetHelper.SetUpDbSetMock<Pokemon>(_pokemons);
            _databaseContext.Setup(dc => dc.Pokemons).Returns(poks.Object);
            var abilities = PokemonDbSetHelper.SetUpDbSetMock<Ability>(_abilities);
            _databaseContext.Setup(dc => dc.Abilities).Returns(abilities.Object);
            var pokAbilities = PokemonDbSetHelper.SetUpDbSetMock<PokemonAbility>(new List<PokemonAbility>());
            _databaseContext.Setup(dc => dc.PokemonAbilities).Returns(pokAbilities.Object);

            _mapper = new PokemonAbilityMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_pokemonDtos, _abilityDtos);
        }

        [TestMethod]
        public void MapsCorrectly_Simple()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokemonDtos[0].abilities.Length, result.Count);
            for(int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_pokemons[0], result[i].Pokemon);
                Assert.AreEqual(_abilities[i], result[i].Ability);
                Assert.AreEqual(_pokemonDtos[0].abilities[i].slot, result[i].Slot);
                Assert.AreEqual(_pokemonDtos[0].abilities[i].is_hidden, result[i].Hidden);
            }
        }
    }
}
