using Models.Pokemons;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        Pokemon GetByName(string name);
        List<Pokemon> GetPokemonsSimilarNames(string name, int levenshteinDistance = 3);
    }
}
