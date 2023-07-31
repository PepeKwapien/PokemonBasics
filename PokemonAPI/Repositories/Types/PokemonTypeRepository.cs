using DataAccess;
using Models.Types;

namespace PokemonAPI.Repositories.Types
{
    public class PokemonTypeRepository : IPokemonTypeRepository
    {
        private readonly IPokemonDatabaseContext _databaseContext;

        public PokemonTypeRepository(IPokemonDatabaseContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }
        public List<DamageMultiplier> GetTypeCharacteristicByName(string typeName)
        {
            return _databaseContext
                .DamageMultipliers
                .AsEnumerable()
                .Where(multiplier => multiplier.Type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) || multiplier.Against.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
