using Models.Pokemons;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        Pokemon[] GetPokemonsWithSimilarName(string name, int take = 3, int levenshteinDistance = 3);
    }
}
