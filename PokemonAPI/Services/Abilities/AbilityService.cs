using PokemonAPI.DTOs;
using PokemonAPI.Repositories;

namespace PokemonAPI.Services
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityRepository _abilityRepository;

        public AbilityService(IAbilityRepository abilityRepository)
        {
            _abilityRepository = abilityRepository;
        }
        public List<AbilityDto> GetAbilitiesDtoForPokemon(string name)
        {
            return _abilityRepository
                .GetAbilitiesForPokemon(name)
                .Select(pa => AbilityDto.FromPokemonAbility(pa))
                .ToList();
        }
    }
}
