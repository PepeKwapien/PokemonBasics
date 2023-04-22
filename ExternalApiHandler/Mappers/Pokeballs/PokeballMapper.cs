using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Models.Generations;
using Models.Pokeballs;

namespace ExternalApiHandler.Mappers
{
    public class PokeballMapper : Mapper<Pokeball>
    {
        private readonly ILogger _logger;
        private List<PokeballDto> _pokeballDtos;
        private List<GenerationDto> _generationDtos;

        public PokeballMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
        }

        public override List<Pokeball> Map()
        {
            List<Pokeball> pokeballs= new List<Pokeball>();

            foreach(PokeballDto pokeballDto in _pokeballDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(pokeballDto.names).name;
                string description = LanguageVersionHelper.FindEnglishVersion(pokeballDto.effect_entries).effect;
                List<Generation> generations= new List<Generation>();
                foreach (var generationInDto in pokeballDto.game_indices)
                {
                    generations.Add(EntityFinderHelper.FindGenerationByDtoName(_dbContext, generationInDto.generation.name, _generationDtos));
                }

                pokeballs.Add(new Pokeball
                {
                    Name = name,
                    Description = description,
                    Generations = generations
                });

                _logger.Debug($"Mapped pokeball {name}");
            }

            foreach (var pokeball in _dbContext.Pokeballs)
            {
                _logger.Debug($"Removing pokeball {pokeball.Name}");
                _dbContext.Pokeballs.Remove(pokeball);
            }

            _dbContext.Pokeballs.AddRange(pokeballs);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokeballs.Count} pokeballs");

            return pokeballs;
        }

        public void SetUp(List<PokeballDto> pokeballs, List<GenerationDto> generations)
        {
            _pokeballDtos = pokeballs;
            _generationDtos = generations;
        }
    }
}
