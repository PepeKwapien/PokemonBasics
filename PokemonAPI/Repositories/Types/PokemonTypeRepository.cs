using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models.Types;

namespace PokemonAPI.Repositories
{
    public class PokemonTypeRepository : IPokemonTypeRepository
    {
        private readonly IPokemonDatabaseContext _databaseContext;

        public PokemonTypeRepository(IPokemonDatabaseContext _databaseContext)
        {
            this._databaseContext = _databaseContext;
        }

        public List<PokemonType> GetAllRelevantTypes()
        {
            return _databaseContext.Types.Where(type => type.Name != "Shadow" && type.Name != "???").ToList();
        }

        public List<DamageMultiplier> GetTypeCharacteristicByName(string typeName)
        {
            return _databaseContext
                .DamageMultipliers
                .Include(dm => dm.Type)
                .Include(dm => dm.Against)
                .AsEnumerable()
                .Where(multiplier => multiplier.Type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) || multiplier.Against.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<DamageMultiplier> GetTypeDefensiveCharacteristicByName(string typeName)
        {
            return _databaseContext
                .DamageMultipliers
                .Include(dm => dm.Type)
                .Include(dm => dm.Against)
                .AsEnumerable()
                .Where(multiplier => multiplier.Against.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
