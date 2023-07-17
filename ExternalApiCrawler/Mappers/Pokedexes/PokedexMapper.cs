using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Enums;
using Models.Pokedexes;

namespace ExternalApiCrawler.Mappers
{
    public class PokedexMapper : Mapper<Pokedex>
    {
        private readonly ILogger _logger;
        private List<PokedexDto> _pokedexDtos;

        public PokedexMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokedexDtos = new List<PokedexDto>();
        }

        public override List<Pokedex> MapToDb()
        {
            List<Pokedex> pokedexes = new List<Pokedex>();

            foreach (var pokedexDto in _pokedexDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(pokedexDto.names)?.name ?? StringHelper.Normalize(pokedexDto.name);
                Regions? region = null;
                if (pokedexDto.region != null)
                {
                    region = EnumHelper.GetEnumValueFromKey<Regions>(pokedexDto.region.name, _logger);
                }
                string description = LanguageVersionHelper.FindEnglishVersion(pokedexDto.descriptions)?.description ?? "-";

                pokedexes.Add(new Pokedex
                {
                    Name = name,
                    Description = description,
                    Region = region
                });

                _logger.Debug($"Mapped pokedex {name}");
            }

            _dbContext.Pokedexes.AddRange(pokedexes);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokedexes.Count} pokedexes");

            return pokedexes;
        }

        public void SetUp(List<PokedexDto> pokedexes)
        {
            _pokedexDtos = pokedexes;
        }
    }
}
