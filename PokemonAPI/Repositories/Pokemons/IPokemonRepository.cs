using Models.Pokemons;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        List<Pokemon> GetPokemonsSimilarNames(string name, int levenshteinDistance = 3);
    }
}
