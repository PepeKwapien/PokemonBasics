using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Generations;
using Models.Pokemons;
using Models.Types;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonMapper : Mapper<Pokemon>
    {
        private readonly ILogger _logger;
        private List<PokemonDto> _pokemonDtos;
        private List<PokemonSpeciesDto> _pokemonSpeciesDtos;
        private List<GenerationDto> _generationDtos;

        public PokemonMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokemonDtos = new List<PokemonDto>();
            _pokemonSpeciesDtos = new List<PokemonSpeciesDto>();
            _generationDtos = new List<GenerationDto>();
        }

        public override List<Pokemon> MapToDb()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (var pokemonDto in _pokemonDtos)
            {
                PokemonSpeciesDto pokemonSpecies = FindMatchingPokemonSpecies(pokemonDto.species.name);
                Generation generation = EntityFinderHelper.FindEntityByDtoName(_dbContext.Generations, pokemonSpecies.generation.name, _generationDtos, _logger);
                string name = StringHelper.Normalize(pokemonDto.name);
                string genera = LanguageVersionHelper.FindEnglishVersion(pokemonSpecies.genera)?.genus ?? "-";
                string? habitat = StringHelper.NormalizeIfNotNull(pokemonSpecies.habitat?.name);
                string? shape = StringHelper.NormalizeIfNotNull(pokemonSpecies.shape?.name);
                PokemonType primaryType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, pokemonDto.types[0].type.name, _logger);
                PokemonType? secondaryType = null;
                if (pokemonDto.types.Length > 1)
                {
                    secondaryType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, pokemonDto.types[1].type.name, _logger);
                }

                int hp = FindStatValue("hp", pokemonDto.stats);
                int attack = FindStatValue("attack", pokemonDto.stats);
                int defense = FindStatValue("defense", pokemonDto.stats);
                int specialAttack = FindStatValue("special-attack", pokemonDto.stats);
                int specialDefense = FindStatValue("special-defense", pokemonDto.stats);
                int speed = FindStatValue("speed", pokemonDto.stats);

                string eggGroups = StringHelper.NormalizeAndJoinNames(pokemonSpecies.egg_groups);

                pokemons.Add(new Pokemon
                {
                    Name = name,
                    PrimaryTypeId = primaryType.Id,
                    PrimaryType = primaryType,
                    SecondaryTypeId = secondaryType?.Id,
                    SecondaryType = secondaryType,
                    HP = hp,
                    Attack = attack,
                    Defense = defense,
                    SpecialAttack = specialAttack,
                    SpecialDefense = specialDefense,
                    Speed = speed,
                    EggGroups = eggGroups,
                    Height = pokemonDto.height,
                    Weight = pokemonDto.weight,
                    Habitat = habitat,
                    Genera = genera,
                    HasGenderDifferences = pokemonSpecies.has_gender_differences,
                    Baby = pokemonSpecies.is_baby,
                    Legendary = pokemonSpecies.is_legendary,
                    Mythical = pokemonSpecies.is_mythical,
                    Shape = shape,
                    GenerationId = generation.Id,
                    Generation = generation
                });

                _logger.Debug($"Mapped pokemon {name}");
            }

            _dbContext.Pokemons.AddRange(pokemons);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemons.Count} pokemons");

            return pokemons;
        }

        public void SetUp(List<PokemonDto> pokemons, List<PokemonSpeciesDto> pokemonSpecies, List<GenerationDto> generations)
        {
            _pokemonDtos = pokemons;
            _pokemonSpeciesDtos = pokemonSpecies;
            _generationDtos = generations;
        }

        private PokemonSpeciesDto FindMatchingPokemonSpecies(string name)
        {
            return _pokemonSpeciesDtos.FirstOrDefault(psd => psd.name.ToLower() == name.ToLower());
        }

        private int FindStatValue(string statName, Stat[] stats)
        {
            return stats.FirstOrDefault(stat => stat.stat.name.Equals(statName))?.base_stat ?? 0;
        }
    }
}
