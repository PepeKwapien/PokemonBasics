using Models.Abilities;

namespace PokemonAPI.Repositories
{
    public interface IAbilityRepository
    {
        List<PokemonAbility> GetAbilitiesForPokemon(string name);
    }
}
