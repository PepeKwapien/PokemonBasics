using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Abilities;
using Models.Pokemons;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonAbilityMapper : Mapper<PokemonAbility>
    {
        private readonly ILogger _logger;
        private List<PokemonDto> _pokemonDtos;
        private List<AbilityDto> _abilityDtos;

        public PokemonAbilityMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokemonDtos = new List<PokemonDto>();
            _abilityDtos = new List<AbilityDto>();
        }

        public override List<PokemonAbility> MapToDb()
        {
            List<PokemonAbility> pokemonAbilities = new List<PokemonAbility>();

            foreach (PokemonDto pokemonDto in _pokemonDtos)
            {
                Pokemon pokemon = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, pokemonDto.name, _logger);

                foreach (PokemonAbilityDto pokemonAbility in pokemonDto.abilities)
                {
                    Ability ability = EntityFinderHelper.FindEntityByDtoName(_dbContext.Abilities, pokemonAbility.ability.name, _abilityDtos, _logger);

                    pokemonAbilities.Add(new PokemonAbility
                    {
                        PokemonId = pokemon.Id,
                        Pokemon = pokemon,
                        AbilityId = ability.Id,
                        Ability = ability,
                        Hidden = pokemonAbility.is_hidden,
                        Slot = pokemonAbility.slot,
                    });
                    _logger.Debug($"Mapped ability {ability.Name} for pokemon {pokemon.Name}");
                }
            }

            _dbContext.PokemonAbilities.AddRange(pokemonAbilities);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemonAbilities.Count} pokemon abilities");

            return pokemonAbilities;
        }

        public void SetUp(List<PokemonDto> pokemons, List<AbilityDto> abilities)
        {
            _pokemonDtos = pokemons;
            _abilityDtos = abilities;
        }
    }
}
