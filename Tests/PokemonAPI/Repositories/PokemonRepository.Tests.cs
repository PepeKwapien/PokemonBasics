using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using Moq;
using PokemonAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using Tests.TestHelpers;

namespace Tests.PokemonAPI.Repositories
{
    [TestClass]
    public class PokemonRepositoryTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<Pokemon> _pokemons;
        private List<PokemonEntry> _pokemonEntries;
        private List<Pokedex> _pokedexes;
        private IPokemonRepository _pokemonRepository;

        [TestInitialize] public void Initialize()
        {
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            Pokedex national = new Pokedex()
            {
                Name = "National"
            };

            _pokedexes = new List<Pokedex>()
            {
                national
            };

            PokemonEntry mareepEntry = new PokemonEntry()
            {
                Pokedex = national,
                Number = 179
            };

            PokemonEntry marillEntry = new PokemonEntry()
            {
                Pokedex = national,
                Number = 183
            };

            PokemonEntry charmanderEntry = new PokemonEntry()
            {
                Pokedex = national,
                Number = 4
            };
            PokemonEntry charmeleonEntry = new PokemonEntry()
            {
                Pokedex = national,
                Number = 5
            };
            PokemonEntry charizardEntry = new PokemonEntry()
            {
                Pokedex = national,
                Number = 6
            };

            _pokemonEntries = new List<PokemonEntry>()
            {
               mareepEntry,
               marillEntry,
               charmanderEntry,
               charmeleonEntry,
               charizardEntry
            };

            _pokemons = new List<Pokemon>()
            {
                new Pokemon()
                {
                    Name = "Mareep",
                    PokemonEntries = new List<PokemonEntry>()
                    {
                        mareepEntry
                    },
                },
                new Pokemon()
                {
                    Name = "Marill",
                    PokemonEntries = new List<PokemonEntry>()
                    {
                        marillEntry
                    },
                },
                new Pokemon()
                {
                    Name = "Charmander",
                    PokemonEntries = new List<PokemonEntry>()
                    {
                        charmanderEntry
                    },
                },
                new Pokemon()
                {
                    Name = "Charmeleon",
                    PokemonEntries = new List<PokemonEntry>()
                    {
                        charmeleonEntry
                    },
                },
                new Pokemon()
                {
                    Name = "Charizard",
                    PokemonEntries = new List<PokemonEntry>()
                    {
                        charizardEntry
                    },
                },
            };

            var pokemons = PokemonDbSetHelper.SetUpDbSetMock<Pokemon>(_pokemons);
            var entries = PokemonDbSetHelper.SetUpDbSetMock<PokemonEntry>(_pokemonEntries);
            var pokedexes = PokemonDbSetHelper.SetUpDbSetMock<Pokedex>(_pokedexes);
            _databaseContext.Setup(db => db.Pokemons).Returns(pokemons.Object);
            _databaseContext.Setup(db => db.PokemonEntries).Returns(entries.Object);
            _databaseContext.Setup(db => db.Pokedexes).Returns(pokedexes.Object);

            _pokemonRepository = new PokemonRepository(_databaseContext.Object);
        }

        [TestMethod]
        public void GetByName_GetsCorrectPokemon()
        {
            // Arrange

            // Act
            Pokemon result = _pokemonRepository.GetByName("charmander");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Charmander", result.Name);
        }

        [TestMethod]
        public void GetPokemonsSimilarNames_GetsSimilarNames()
        {
            // Arrange

            // Act
            var result = _pokemonRepository.GetPokemonsSimilarNames("mareil");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(search => search.Name.Equals("Mareep")));
            Assert.IsTrue(result.Any(search => search.Name.Equals("Marill")));
        }

        [TestMethod]
        public void GetPokemonsSimilarNames_ReturnsEmptyListIfNotFound()
        {
            // Arrange

            // Act
            var result = _pokemonRepository.GetPokemonsSimilarNames("complete jiberish");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetPokemonsSimilarNames_GetsIfNameContainsPhrase()
        {
            // Arrange

            // Act
            var result = _pokemonRepository.GetPokemonsSimilarNames("char");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(search => search.Name.Equals("Charmander")));
            Assert.IsTrue(result.Any(search => search.Name.Equals("Charmeleon")));
            Assert.IsTrue(result.Any(search => search.Name.Equals("Charizard")));
        }
    }
}
