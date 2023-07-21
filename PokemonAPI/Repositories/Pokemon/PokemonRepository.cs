using DataAccess;
using PokemonAPI.Helpers;
using System.Linq;

namespace PokemonAPI.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonDatabaseContext _databaseContext;

        public PokemonRepository(IPokemonDatabaseContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }
        public string[] GetPokemonsWithSimilarName(string name, int take = 3, int levenshteinDistance = 3)
        {
            return _databaseContext.Pokemons
                .Select(pokemon => pokemon.Name.ToLower())
                .AsEnumerable()
                .Where(pokemonName => pokemonName.Contains(name, StringComparison.OrdinalIgnoreCase) || StringHelper.LevenshteinDistance(name, pokemonName) < levenshteinDistance)
                .Take(take)
                .ToArray();
        }
    }
}
