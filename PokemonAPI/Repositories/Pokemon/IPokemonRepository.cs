using PokemonAPI.DTO;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        PokemonSearchItemDto[] GetPokemonsSearchItemsWithSimilarNames(string name, int levenshteinDistance = 3);
    }
}
