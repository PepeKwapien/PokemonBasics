using PokemonAPI.DTO;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        PokemonSearchItem[] GetPokemonsSearchItemsWithSimilarNames(string name, int levenshteinDistance = 3);
    }
}
