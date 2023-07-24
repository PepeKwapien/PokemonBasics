using PokemonAPI.DTO;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        PokemonSearchItem[] GetPokemonsSearchItemsWithSimilarNames(string name, int take = 3, int levenshteinDistance = 3);
    }
}
