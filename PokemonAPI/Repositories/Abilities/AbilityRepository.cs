using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models.Abilities;

namespace PokemonAPI.Repositories
{
    public class AbilityRepository : IAbilityRepository
    {
        private readonly IPokemonDatabaseContext _databaseContext;

        public AbilityRepository(IPokemonDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<PokemonAbility> GetAbilitiesForPokemon(string name)
        {
            return _databaseContext
                .PokemonAbilities
                .Include(pa => pa.Pokemon)
                .Include(pa => pa.Ability)
                .AsEnumerable()
                .Where(pa => pa.Pokemon.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
