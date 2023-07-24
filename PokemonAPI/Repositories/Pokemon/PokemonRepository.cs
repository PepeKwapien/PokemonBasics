using DataAccess;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.DTO;
using PokemonAPI.Helpers;

namespace PokemonAPI.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonDatabaseContext _databaseContext;

        public PokemonRepository(IPokemonDatabaseContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }
        public PokemonSearchItem[] GetPokemonsSearchItemsWithSimilarNames(string name, int takeCount = 3, int levenshteinDistance = 3)
        {
            // Explicit import so that DB context knows references to collections
            _databaseContext.PokemonEntries.Include(entry => entry.Pokedex).ToList();

            return _databaseContext.Pokemons
                .AsEnumerable()
                .Where(pokemon => pokemon.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || StringHelper.LevenshteinDistance(name, pokemon.Name) < levenshteinDistance)
                .Take(takeCount)
                .Select(pokemon => PokemonSearchItem.FromPokemon(pokemon))
                .ToArray();
        }
    }
}
