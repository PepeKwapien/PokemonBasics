using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Models.Enums;
using Models.Generations;

namespace ExternalApiHandler.Mappers
{
    public class GenerationMapper : Mapper<Generation>
    {
        private readonly ILogger _logger;
        private List<GenerationDto> _generationDtos;

        public GenerationMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
        }
        public override List<Generation> Map()
        {
            List<Generation> generations= new List<Generation>();

            foreach(GenerationDto generationDto in _generationDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(generationDto.names).name;
                Regions region = EnumHelper.GetEnumValueFromKey<Regions>(generationDto.main_region.name);

                generations.Add(new Generation
                {
                    Name = name,
                    Region = region,
                });
                _logger.Debug($"Mapped generation {name} in a region {region}");
            }

            foreach(var generation in _dbContext.Generations)
            {
                _logger.Debug($"Removing generation {generation.Name}");
                _dbContext.Generations.Remove(generation);
            }

            _dbContext.Generations.AddRange(generations);
            _dbContext.SaveChanges();
            _logger.Success($"Saved {generations.Count} generations");

            return generations;
        }

        public void SetUp(List<GenerationDto> generationDtos)
        {
            _generationDtos = generationDtos;
        }
    }
}
