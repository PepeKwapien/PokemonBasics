using PokemonAPI.DTOs;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        PokemonSearchItemDto[] GetPokemonsSearchItemsWithSimilarNames(string name, int levenshteinDistance = 3);
    }
}
