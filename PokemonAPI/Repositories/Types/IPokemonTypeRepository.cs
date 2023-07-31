using Models.Types;

namespace PokemonAPI.Repositories.Types
{
    public interface IPokemonTypeRepository
    {
        List<PokemonType> GetAllRelevantTypes();
        List<DamageMultiplier> GetTypeCharacteristicByName(string typeName);
        List<DamageMultiplier> GetTypeDefensiveCharacteristicByName(string typeName);
    }
}
