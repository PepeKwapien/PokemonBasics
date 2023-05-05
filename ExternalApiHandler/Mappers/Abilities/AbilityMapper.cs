using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Abilities;
using Models.Generations;
using System.Text.RegularExpressions;

namespace ExternalApiCrawler.Mappers
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
            _generationDtos = new List<GenerationDto>();
        }

        public override List<Ability> MapToDb()
        {
            List<Ability> abilities = new List<Ability>();

            foreach (var abilityDto in _abilityDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(abilityDto.names).name;
                Generation generation = EntityFinderHelper.FindEntityByDtoName(_dbContext.Generations, abilityDto.generation.name, _generationDtos, _logger);
                string rawEffectEntry = LanguageVersionHelper.FindEnglishVersion(abilityDto.effect_entries)?.effect ?? "-";
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
