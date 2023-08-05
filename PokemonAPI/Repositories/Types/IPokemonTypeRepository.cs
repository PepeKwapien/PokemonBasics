using Models.Types;

namespace PokemonAPI.Repositories
{
    public interface IPokemonTypeRepository
    {
        List<PokemonType> GetAllRelevantTypes();
        PokemonType GetTypeByName(string name);
        List<DamageMultiplier> GetTypeCharacteristicByName(string typeName);
        List<DamageMultiplier> GetTypeDefensiveCharacteristicByName(string typeName);
    }
}
