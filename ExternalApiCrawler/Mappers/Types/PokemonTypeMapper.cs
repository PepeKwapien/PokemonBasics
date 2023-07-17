using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Types;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonTypeMapper : Mapper<PokemonType>
    {
        private readonly ILogger _logger;
        private List<PokemonTypeDto> _pokemonTypesDto;

        public PokemonTypeMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokemonTypesDto = new List<PokemonTypeDto>();
        }

        public override List<PokemonType> MapToDb()
        {
            List<PokemonType> pokemonTypes = new List<PokemonType>();

            foreach (PokemonTypeDto typeDto in _pokemonTypesDto)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(typeDto.names).name;
                string color = TypeColorHelper.GetTypeColor(typeDto.name);

                PokemonType newType = new PokemonType()
                {
                    Name = name,
                    Color = color,
                };

                pokemonTypes.Add(newType);
                _logger.Debug($"Mapped type {name} with color {color}");
            }

            _dbContext.Types.AddRange(pokemonTypes);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemonTypes.Count} types");

            return pokemonTypes;
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }
    }
}
