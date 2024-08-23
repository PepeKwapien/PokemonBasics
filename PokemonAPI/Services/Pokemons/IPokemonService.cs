using Models.Pokemons;
using PokemonAPI.DTOs;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Pokemon GetPokemonByName(string name);
        PokemonSearchItemDto[] GetPokemonsSearchItemsWithSimilarNames(string name, int levenshteinDistance = 3);
        Pokemon[] GetRandomPokemons(int size);
    }
}
