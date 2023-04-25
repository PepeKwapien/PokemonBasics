using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Models.Generations;
using Models.Pokedexes;
using Models.Pokemons;
using Moq;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class PokemonEntryMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokedexDto> _pokedexDtos;
        private List<PokemonSpeciesDto> _speciesDto;
        private List<Pokedex> _pokedexes;
        private List<Pokemon> _pokemons;
        private PokemonEntryMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _speciesDto = new List<PokemonSpeciesDto>()
            {
                new PokemonSpeciesDto
                {
                    name = "first",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "First"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "second",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "Second"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "third",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "Third1"
                            }
                        },
                        new Variety
                        {
                            is_default = false,
                            pokemon = new Name
                            {
                                name = "Third2"
                            }
                        },
                        new Variety
                        {
                            is_default = false,
                            pokemon = new Name
                            {
                                name = "Third3"
                            }
                        },
                    }
                },
            };
            _pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "First",
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Second",
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Third1",
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Third2",
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Third3",
                },
            };

            var poks = PokemonDbSetHelper.SetUpDbSetMock<Pokemon>(_pokemons);
            _databaseContext.Setup(dc => dc.Pokemons).Returns(poks.Object);
            var entries = PokemonDbSetHelper.SetUpDbSetMock<PokemonEntry>(new List<PokemonEntry>());
            _databaseContext.Setup(dc => dc.PokemonEntries).Returns(entries.Object);

            _mapper = new PokemonEntryMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void MapsOneEntryFromOnePokedex()
        {
            // Arrange
            _pokedexDtos = new List<PokedexDto>
            {
                new PokedexDto
                {
                    name = "1",
                    names = SingleEnglishNameHelper.Generate("Very first"),
                    pokemon_entries = new PokemonEntryDto[]
                    {
                        new PokemonEntryDto
                        {
                            entry_number = 10,
                            pokemon_species = new Name
                            {
                                name = "first"
                            }
                        }
                    }
                }
            };

            _pokedexes = new List<Pokedex>()
            {
                new Pokedex
                {
                    Id = Guid.NewGuid(),
                    Name = "Very first"
                }
            };

            var dexes = PokemonDbSetHelper.SetUpDbSetMock<Pokedex>(_pokedexes);
            _databaseContext.Setup(dc => dc.Pokedexes).Returns(dexes.Object);

            _mapper.SetUp(_pokedexDtos, _speciesDto);

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_pokedexes[0], result[0].Pokedex);
            Assert.AreEqual(_pokemons[0], result[0].Pokemon);
            Assert.AreEqual(10, result[0].Number);
        }

        [TestMethod]
        public void MapsOneEntryFromTwoPokedexes()
        {
            // Arrange
            _pokedexDtos = new List<PokedexDto>
            {
                new PokedexDto
                {
                    name = "1",
                    names = SingleEnglishNameHelper.Generate("Very first"),
                    pokemon_entries = new PokemonEntryDto[]
                    {
                        new PokemonEntryDto
                        {
                            entry_number = 10,
                            pokemon_species = new Name
                            {
                                name = "first"
                            }
                        }
                    }
                },
                new PokedexDto
                {
                    name = "2",
                    names = SingleEnglishNameHelper.Generate("Very second"),
                    pokemon_entries = new PokemonEntryDto[]
                    {
                        new PokemonEntryDto
                        {
                            entry_number = 20,
                            pokemon_species = new Name
                            {
                                name = "second"
                            }
                        }
                    }
                }
            };

            _pokedexes = new List<Pokedex>()
            {
                new Pokedex
                {
                    Id = Guid.NewGuid(),
                    Name = "Very first"
                },
                new Pokedex
                {
                    Id = Guid.NewGuid(),
                    Name = "Very second"
                }
            };

            var dexes = PokemonDbSetHelper.SetUpDbSetMock<Pokedex>(_pokedexes);
            _databaseContext.Setup(dc => dc.Pokedexes).Returns(dexes.Object);

            _mapper.SetUp(_pokedexDtos, _speciesDto);

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            for(int i = 0; i < 2; i++)
            {
                Assert.AreEqual(_pokedexes[i], result[i].Pokedex);
                Assert.AreEqual(_pokemons[i], result[i].Pokemon);
            }
        }

        [TestMethod]
        public void MapsThreeEntriesFromSpeciesFromOnePokedex()
        {
            // Arrange
            _pokedexDtos = new List<PokedexDto>
            {
                new PokedexDto
                {
                    name = "3",
                    names = SingleEnglishNameHelper.Generate("Very third"),
                    pokemon_entries = new PokemonEntryDto[]
                    {
                        new PokemonEntryDto
                        {
                            entry_number = 10,
                            pokemon_species = new Name
                            {
                                name = "third"
                            }
                        }
                    }
                },
            };

            _pokedexes = new List<Pokedex>()
            {
                new Pokedex
                {
                    Id = Guid.NewGuid(),
                    Name = "Very third"
                },
            };

            var dexes = PokemonDbSetHelper.SetUpDbSetMock<Pokedex>(_pokedexes);
            _databaseContext.Setup(dc => dc.Pokedexes).Returns(dexes.Object);

            _mapper.SetUp(_pokedexDtos, _speciesDto);

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(_pokedexes[0], result[i].Pokedex);
                Assert.AreEqual(_pokemons[2+i], result[i].Pokemon);
            }
        }
    }
}
