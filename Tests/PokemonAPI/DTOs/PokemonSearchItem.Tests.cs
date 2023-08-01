using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Pokedexes;
using Models.Pokemons;
using PokemonAPI.DTO;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class PokemonSearchItemTests
    {
        [TestMethod]
        public void PokemonSearchItem_FromPokemon()
        {
            // Arrange
            Pokemon pokemon = new Pokemon()
            {
                Name = "Bulbasaur",
                Sprite = "Bulbasaur.png"
            };
            Pokedex pokedex = new Pokedex()
            {
                Name = "National"
            };
            PokemonEntry entry = new PokemonEntry()
            {
                Pokedex = pokedex,
                Number = 1
            };
            pokemon.PokemonEntries.Add(entry);

            PokemonSearchItem expectedSearchItem = new PokemonSearchItem()
            {
                Name = pokemon.Name,
                Image = pokemon.Sprite,
                Number = entry.Number
            };

            // Act
            PokemonSearchItem actualSearchItem = PokemonSearchItem.FromPokemon(pokemon);

            // Assert
            Assert.AreEqual(expectedSearchItem, actualSearchItem);
        }
    }
}
