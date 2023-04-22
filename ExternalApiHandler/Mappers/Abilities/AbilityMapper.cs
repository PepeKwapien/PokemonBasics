using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Models.Abilities;
using Models.Generations;
using Models.Pokeballs;
using System.Text.RegularExpressions;

namespace ExternalApiHandler.Mappers
{
    public class AbilityMapper : Mapper<Ability>
    {
        private readonly ILogger _logger;
        private List<AbilityDto> _abilityDtos;
        private List<GenerationDto> _generationDtos;

        public AbilityMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _abilityDtos = new List<AbilityDto>();
        }

        public override List<Ability> Map()
        {
            List<Ability> abilities= new List<Ability>();

            foreach(var abilityDto in _abilityDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(abilityDto.names).name;
                Generation generation = EntityFinderHelper.FindGenerationByDtoName(_dbContext, abilityDto.generation.name, _generationDtos);
                string rawEffectEntry = LanguageVersionHelper.FindEnglishVersion(abilityDto.effect_entries)?.effect ?? "";
                string[] effectEntriesInBattleAndOverworld = Regex.Split(rawEffectEntry, @"\s*Overworld:\s*");
                abilities.Add(new Ability
                {
                    Name = name,
                    GenerationId = generation.Id,
                    Generation = generation,
                    Effect = effectEntriesInBattleAndOverworld[0],
                    OverworldEffect = effectEntriesInBattleAndOverworld.Length > 1 ? effectEntriesInBattleAndOverworld[1] : null,
                });

                _logger.Debug($"Mapped ability {name}");
            }

            foreach (var ability in _dbContext.Abilities)
            {
                _logger.Debug($"Removing ability {ability.Name}");
                _dbContext.Abilities.Remove(ability);
            }

            _dbContext.Abilities.AddRange(abilities);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {abilities.Count} abilities");

            return abilities;
        }

        public void SetUp(List<AbilityDto> pokemonAbilities, List<GenerationDto> generations)
        {
            _abilityDtos = pokemonAbilities;
            _generationDtos = generations;
        }
    }
}
