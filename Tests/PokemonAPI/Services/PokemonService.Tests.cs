using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using Moq;
using PokemonAPI.Repositories;
using PokemonAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace Tests.PokemonAPI.Services
{
    [TestClass]
    public class PokemonServiceTests
    {
        private Mock<IPokemonRepository> _pokemonRepository;
        private PokemonService _pokemonService;

        [TestInitialize]
        public void Initialize()
        {
            _pokemonRepository = new Mock<IPokemonRepository>();
            _pokemonService = new PokemonService(_pokemonRepository.Object);
        }

        [TestMethod]
        public void GetPokemonsSearchItemsWithSimilarNames_MapsCorrectly()
        {
            // Assert
            Pokedex national = new Pokedex()
            {
                Name = "National"
            };

            List<Pokemon> pokemons = new List<Pokemon>()
            {
                new Pokemon()
                {
                    Name = "Bulbasaur",
                    PokemonEntries= new List<PokemonEntry>()
                    {
                        new PokemonEntry()
                        {
                            Pokedex = national,
                            Number = 1
                        }
                    },
                    Sprite = "bulbasaur.png"
                },
                new Pokemon()
                {
                    Name = "Charmander",
                    PokemonEntries= new List<PokemonEntry>()
                    {
                        new PokemonEntry()
                        {
                            Pokedex = national,
                            Number = 4
                        }
                    },
                    Sprite = "charmander.png"
                },
                new Pokemon()
                {
                    Name = "Squirtle",
                    PokemonEntries= new List<PokemonEntry>()
                    {
                        new PokemonEntry()
                        {
                            Pokedex = national,
                            Number = 7
                        }
                    },
                    Sprite = "squirtle.png"
                },
            };

            _pokemonRepository.Setup(pr => pr.GetPokemonsSimilarNames(It.IsAny<string>(), It.IsAny<int>())).Returns(pokemons);

            // Act
            var result = _pokemonService.GetPokemonsSearchItemsWithSimilarNames("kanto starters");

            // Arrange
            Assert.IsNotNull(result);
            Assert.AreEqual(pokemons.Count, result.Length);
            foreach(var pokemon in pokemons)
            {
                Assert.IsTrue(result.Any(searchItem => searchItem.Name == pokemon.Name && searchItem.Image == pokemon.Sprite && searchItem.Number == pokemon.PokemonEntries.First().Number));
            }
        }
    }
}
