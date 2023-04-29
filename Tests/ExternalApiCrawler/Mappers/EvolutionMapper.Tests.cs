using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokemons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class EvolutionMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonSpeciesDto> _speciesDtos;
        private List<Pokemon> _pokemons;
        private EvolutionMapper _mapper;
        private EvolvesTo[] _defaultEvolvesTo;

        [TestInitialize]
        public void Initialize()
        {
            _defaultEvolvesTo = new EvolvesTo[0];
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            _speciesDtos = new List<PokemonSpeciesDto>()
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
                },
                new PokemonSpeciesDto
                {
                    name = "ivysaur",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "ivysaur"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "venusaur",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "venusaur"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "wurmple",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "wurmple"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "silcoon",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "silcoon"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "beautifly",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "beautifly"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "cascoon",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "cascoon"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "dustox",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "dustox"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "corsola",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "corsola"
                            }
                        },
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "corsola-galar"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "cursola",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "cursola"
                            }
                        },
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "slowpoke",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowpoke"
                            }
                        },
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowpoke-galar"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "slowbro",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowbro"
                            }
                        },
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowbro-galar"
                            }
                        }
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "slowking",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowking"
                            }
                        },
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "slowking-galar"
                            }
                        }
                    }
                },
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
                                name = "meowth-alola"
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
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "persian",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "persian"
                            }
                        },
                        new Variety
                        {
                            is_default = false,
                            pokemon = new Name
                            {
                                name = "persian-alola"
                            }
                        },
                    }
                },
                new PokemonSpeciesDto
                {
                    name = "perrserker",
                    varieties = new Variety[]
                    {
                        new Variety
                        {
                            is_default = true,
                            pokemon = new Name
                            {
                                name = "perrserker"
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
                    Name = "Bulbasaur"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Ivysaur"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Venusaur"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Wurmple"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Cascoon"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Silcoon"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Dustox"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Beautifly"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Corsola"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Corsola Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Cursola"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowpoke"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowpoke Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowbro"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowbro Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowking"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Slowking Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth Alola"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Meowth Galar"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Persian"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Persian Alola"
                },
                new Pokemon
                {
                    Id = Guid.NewGuid(),
                    Name = "Perrserker"
                },
            };

            var poks = PokemonDbSetHelper.SetUpDbSetMock(_pokemons);
            _databaseContext.Setup(dc => dc.Pokemons).Returns(poks.Object);
            var evos = PokemonDbSetHelper.SetUpDbSetMock(new List<Evolution>());
            _databaseContext.Setup(dc => dc.Evolutions).Returns(evos.Object);

            _mapper = new EvolutionMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void MapsCorrectly_SimpleSinlgeRoute()
        {
            // Arrange
            Pokemon bulbasaur = _pokemons[0];
            Pokemon ivysaur = _pokemons[1];
            Pokemon venusaur = _pokemons[2];

            List<EvolutionChainDto> evolutionChainDtos = new List<EvolutionChainDto>()
            {
                new EvolutionChainDto
                {
                    baby_trigger_item = null,
                    chain = CreateEvolvesTo("bulbasaur",
                        new EvolvesTo[] {CreateEvolvesTo("ivysaur",
                            new EvolvesTo[]{CreateEvolvesTo("venusaur", new EvolvesTo[0])}
                            )})
                }
            };

            _mapper.SetUp(_speciesDtos, evolutionChainDtos, new List<MoveDto>());

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == bulbasaur && evolution.Into == ivysaur));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == ivysaur && evolution.Into == venusaur));
        }

        [TestMethod]
        public void MapsCorrectly_DoubleRoute()
        {
            // Arrange
            EvolvesTo beautiflyEvo = CreateEvolvesTo("beautifly", new EvolvesTo[0]);
            EvolvesTo dustoxEvo = CreateEvolvesTo("dustox", new EvolvesTo[0]);
            EvolvesTo silcoonEvo = CreateEvolvesTo("silcoon", new EvolvesTo[] { beautiflyEvo });
            EvolvesTo cascoonEvo = CreateEvolvesTo("cascoon", new EvolvesTo[] { dustoxEvo });
            EvolvesTo wurmpleEvo = CreateEvolvesTo("wurmple", new EvolvesTo[] { silcoonEvo, cascoonEvo });

            Pokemon beautifly = _pokemons[7];
            Pokemon dustox = _pokemons[6];
            Pokemon silcoon = _pokemons[5];
            Pokemon cascoon = _pokemons[4];
            Pokemon wurmple = _pokemons[3];

            List<EvolutionChainDto> evolutionChainDtos = new List<EvolutionChainDto>()
            {
                new EvolutionChainDto
                {
                    baby_trigger_item = null,
                    chain = wurmpleEvo
                }
            };

            _mapper.SetUp(_speciesDtos, evolutionChainDtos, new List<MoveDto>());

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == wurmple && evolution.Into == cascoon));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == wurmple && evolution.Into == silcoon));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == cascoon && evolution.Into == dustox));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == silcoon && evolution.Into == beautifly));
        }

        [TestMethod]
        public void MapsCorrectly_RegionalEvolution()
        {
            // Arrange
            EvolvesTo cursolaEvo = CreateEvolvesTo("cursola", new EvolvesTo[0]);
            EvolvesTo corsolaEvo = CreateEvolvesTo("corsola", new EvolvesTo[] {cursolaEvo});

            Pokemon corsolaGalar = _pokemons[9];
            Pokemon cursola = _pokemons[10];

            List<EvolutionChainDto> evolutionChainDtos = new List<EvolutionChainDto>()
            {
                new EvolutionChainDto
                {
                    baby_trigger_item = null,
                    chain = corsolaEvo
                }
            };

            _mapper.SetUp(_speciesDtos, evolutionChainDtos, new List<MoveDto>());

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == corsolaGalar && evolution.Into == cursola));
        }

        [TestMethod]
        public void MapsCorrectly_ParalelDoubleRoute()
        {
            // Arrange
            EvolvesTo slowbroEvo = CreateEvolvesTo("slowbro", new EvolvesTo[0]);
            slowbroEvo.evolution_details = new EvolutionDetail[]
            {
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "level-up"
                    }
                },
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "galarian jelly"
                    }
                }
            };
            EvolvesTo slowkingEvo = CreateEvolvesTo("slowking", new EvolvesTo[0]);
            slowkingEvo.evolution_details = new EvolutionDetail[]
            {
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "level-up"
                    }
                },
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "galarian jelly"
                    }
                }
            };
            EvolvesTo slowpokeEvo = CreateEvolvesTo("slowpoke", new EvolvesTo[] {slowbroEvo, slowkingEvo});

            Pokemon slowpoke = _pokemons[11];
            Pokemon slowpokeGalar = _pokemons[12];
            Pokemon slowbro = _pokemons[13];
            Pokemon slowbroGalar = _pokemons[14];
            Pokemon slowking = _pokemons[15];
            Pokemon slowkingGalar = _pokemons[16];

            List<EvolutionChainDto> evolutionChainDtos = new List<EvolutionChainDto>()
            {
                new EvolutionChainDto
                {
                    baby_trigger_item = null,
                    chain = slowpokeEvo
                }
            };

            _mapper.SetUp(_speciesDtos, evolutionChainDtos, new List<MoveDto>());

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == slowpoke && evolution.Into == slowbro && evolution.Trigger == "Level Up"));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == slowpokeGalar && evolution.Into == slowbroGalar && evolution.Trigger == "Galarian Jelly"));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == slowpoke && evolution.Into == slowking && evolution.Trigger == "Level Up"));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == slowpokeGalar && evolution.Into == slowkingGalar && evolution.Trigger == "Galarian Jelly"));
        }

        [TestMethod]
        public void MapsCorrectly_JustMeowth()
        {
            // Arrange
            EvolvesTo persianEvo = CreateEvolvesTo("persian", new EvolvesTo[0]);
            persianEvo.evolution_details = new EvolutionDetail[]
            {
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "level-up"
                    }
                },
                new EvolutionDetail
                {
                    trigger = new Name
                    {
                        name = "catnip"
                    }
                }
            };
            EvolvesTo perrserkerEvo = CreateEvolvesTo("perrserker", new EvolvesTo[0]);
            EvolvesTo meowthEvo = CreateEvolvesTo("meowth", new EvolvesTo[] { persianEvo, perrserkerEvo });

            Pokemon meowth = _pokemons[17];
            Pokemon meowthAlola = _pokemons[18];
            Pokemon meowthGalar = _pokemons[19];
            Pokemon persian = _pokemons[20];
            Pokemon persianAlola = _pokemons[21];
            Pokemon perrserker = _pokemons[22];

            List<EvolutionChainDto> evolutionChainDtos = new List<EvolutionChainDto>()
            {
                new EvolutionChainDto
                {
                    baby_trigger_item = null,
                    chain = meowthEvo
                }
            };

            _mapper.SetUp(_speciesDtos, evolutionChainDtos, new List<MoveDto>());

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == meowth && evolution.Into == persian && evolution.Trigger == "Level Up"));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == meowthAlola && evolution.Into == persianAlola && evolution.Trigger == "Catnip"));
            Assert.IsTrue(result.Any(evolution => evolution.Pokemon == meowthGalar && evolution.Into == perrserker && evolution.Trigger == "Level Up"));
        }

        private EvolvesTo CreateEvolvesTo(string speciesName, EvolvesTo[] evolvesTo, string? trigger = "level-up")
        {
            return new EvolvesTo
            {
                species = new Name
                {
                    name = speciesName,
                },
                evolution_details = trigger == null ? new EvolutionDetail[0] : new EvolutionDetail[]
                {
                    new EvolutionDetail
                    {
                        trigger = new Name
                        {
                            name = trigger,
                        }
                    }
                },
                evolves_to = evolvesTo
            };
        }
    }
}
