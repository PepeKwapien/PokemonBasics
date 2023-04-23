using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Generations;
using Models.Moves;
using Models.Pokemons;
using Models.Types;
using Moq;
using System;
using System.Collections.Generic;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class PokemonMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GenerationDto> _generationDtos;
        private List<PokemonSpeciesDto> _pokemonSpeciesDtos;
        private List<PokemonDto> _pokemonDtos;
        private List<Generation> _generations;
        private Pokemon _expectedPokemon;
        private List<PokemonType> _pokemonTypes;
        private PokemonMapper _mapper;

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
                    Name = "Grass"
                },
                new PokemonType
                {
                    Id = Guid.NewGuid(),
                    Name = "Poison"
                }
            };

            _pokemonDtos = new List<PokemonDto>()
            {
                new PokemonDto
                {
                    height = 178,
                    weight = 880,
                    is_default = true,
                    name = "bulbasaur",
                    order = 1,
                    types = new InnerPokemonType[]
                    {
                        new InnerPokemonType
                        {
                            type = new Name
                            {
                                name = "grass"
                            }
                        },
                        new InnerPokemonType
                        {
                            type = new Name
                            {
                                name = "poison"
                            }
                        }
                    },
                    stats = new Stat[]
                    {
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "hp"
                            },
                            base_stat = 10
                        },
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "attack"
                            },
                            base_stat = 20
                        },
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "defense"
                            },
                            base_stat = 30
                        },
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "special-attack"
                            },
                            base_stat = 40
                        },
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "special-defense"
                            },
                            base_stat = 50
                        },
                        new Stat()
                        {
                            stat = new Name
                            {
                                name = "speed"
                            },
                            base_stat = 60
                        },
                    },
                    species = new Name
                    {
                        name = "bulba"
                    },
                }
            };

            _pokemonSpeciesDtos = new List<PokemonSpeciesDto>
            {
                new PokemonSpeciesDto
                {
                    egg_groups = new Name[]
                    {
                        new Name
                        {
                            name = "polite-boy"
                        },
                        new Name
                        {
                            name = "cute_and elegant"
                        },
                    },
                    generation = new Name
                    {
                        name = "favorite"
                    },
                    genera = new Genera[]
                    {
                        new Genera
                        {
                            genus = "Bulba boy",
                            language = new Name
                            {
                                name = "en"
                            }
                        }
                    },
                    habitat = new Name
                    {
                        name = "my-room"
                    },
                    has_gender_differences = true,
                    is_baby = true,
                    is_legendary = true,
                    is_mythical = true,
                    name = "bulba",
                    shape = new Name
                    {
                        name = "round"
                    }
                },
            };

            _expectedPokemon = new Pokemon
            {
                Name = "Bulbasaur",
                PrimaryTypeId = _pokemonTypes[0].Id,
                PrimaryType = _pokemonTypes[0],
                SecondaryTypeId = _pokemonTypes[1].Id,
                SecondaryType = _pokemonTypes[1],
                Order = 1,
                HP = 10,
                Attack = 20,
                Defense = 30,
                SpecialAttack = 40,
                SpecialDefense = 50,
                Speed = 60,
                Height = 178,
                Weight = 880,
                Habitat = "My Room",
                EggGroups = "Polite Boy, Cute And Elegant",
                Genera = "Bulba boy",
                HasGenderDifferences = true,
                Baby = true,
                Legendary = true,
                Mythical = true,
                Shape = "Round",
                GenerationId = _generations[0].Id,
                Generation = _generations[0]
            };

            var gens = PokemonDatabaseContextMock.SetUpDbSetMock<Generation>(_generations);
            _databaseContext.Setup(dc => dc.Generations).Returns(gens.Object);
            var types = PokemonDatabaseContextMock.SetUpDbSetMock<PokemonType>(_pokemonTypes);
            _databaseContext.Setup(dc => dc.Types).Returns(types.Object);
            var pokemons = PokemonDatabaseContextMock.SetUpDbSetMock<Pokemon>(new List<Pokemon>());
            _databaseContext.Setup(dc => dc.Pokemons).Returns(pokemons.Object);

            _mapper = new PokemonMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_pokemonDtos, _pokemonSpeciesDtos, _generationDtos);
        }

        [TestMethod]
        public void MapsCorrectlyWithBothTypes()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokemonDtos.Count, result.Count);
            Assert.AreEqual(_expectedPokemon.Name, result[0].Name);
            Assert.AreEqual(_expectedPokemon.PrimaryTypeId, result[0].PrimaryTypeId);
            Assert.AreEqual(_expectedPokemon.SecondaryTypeId, result[0].SecondaryTypeId);
            Assert.AreEqual(_expectedPokemon.Order, result[0].Order);
            Assert.AreEqual(_expectedPokemon.HP, result[0].HP);
            Assert.AreEqual(_expectedPokemon.Attack, result[0].Attack);
            Assert.AreEqual(_expectedPokemon.Defense, result[0].Defense);
            Assert.AreEqual(_expectedPokemon.SpecialAttack, result[0].SpecialAttack);
            Assert.AreEqual(_expectedPokemon.SpecialDefense, result[0].SpecialDefense);
            Assert.AreEqual(_expectedPokemon.Speed, result[0].Speed);
            Assert.AreEqual(_expectedPokemon.Height, result[0].Height);
            Assert.AreEqual(_expectedPokemon.Weight, result[0].Weight);
            Assert.AreEqual(_expectedPokemon.Habitat, result[0].Habitat);
            Assert.AreEqual(_expectedPokemon.EggGroups, result[0].EggGroups);
            Assert.AreEqual(_expectedPokemon.Genera, result[0].Genera);
            Assert.AreEqual(_expectedPokemon.HasGenderDifferences, result[0].HasGenderDifferences);
            Assert.AreEqual(_expectedPokemon.Baby, result[0].Baby);
            Assert.AreEqual(_expectedPokemon.Legendary, result[0].Legendary);
            Assert.AreEqual(_expectedPokemon.Mythical, result[0].Mythical);
            Assert.AreEqual(_expectedPokemon.Shape, result[0].Shape);
            Assert.AreEqual(_expectedPokemon.GenerationId, result[0].GenerationId);
        }

        [TestMethod]
        public void MapsCorrectlyWithEmptySecondaryType()
        {
            // Arrange
            _pokemonDtos[0].types = new InnerPokemonType[]
            {
                _pokemonDtos[0].types[0]
            };

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokemonDtos.Count, result.Count);
            Assert.IsNull(result[0].SecondaryTypeId);
            Assert.IsNull(result[0].SecondaryType);
        }
    }
}
