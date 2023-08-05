using PokemonAPI.DTOs;

namespace PokemonAPI.Services
{
    public interface IAbilityService
    {
        List<AbilityDto> GetAbilitiesDtoForPokemon(string name);
    }
}
