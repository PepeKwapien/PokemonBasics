using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
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

        public Pokemon GetByName(string name)
        {
            // Explicit import so that DB context knows references to collections
            _databaseContext.PokemonEntries.Include(entry => entry.Pokedex);
            return _databaseContext.Pokemons
                .Include(pokemon => pokemon.PrimaryType)
                .Include(pokemon => pokemon.SecondaryType)
                .AsEnumerable()
                .FirstOrDefault(pokemon => pokemon.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Pokemon> GetPokemonsSimilarNames(string name, int levenshteinDistance = 3)
        {
            // Explicit import so that DB context knows references to collections
            _databaseContext.PokemonEntries.Include(entry => entry.Pokedex);

            return _databaseContext.Pokemons
                .AsEnumerable()
                .Where(pokemon => pokemon.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || StringHelper.LevenshteinDistance(name, pokemon.Name) < levenshteinDistance)
                .ToList();
        }

        public Pokemon[] GetRandomPokemons(int size)
        {
            return _databaseContext.Pokemons
                .AsEnumerable()
                .OrderBy(pokemon => Guid.NewGuid())
                .Take(size)
                .ToArray();
        }
    }
}
