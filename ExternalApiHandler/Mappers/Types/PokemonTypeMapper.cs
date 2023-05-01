using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Types;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonTypeMapper : Mapper
    {
        private readonly ILogger _logger;
        private List<PokemonTypeDto> _pokemonTypesDto;

        public PokemonTypeMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokemonTypesDto = new List<PokemonTypeDto>();
        }

        public List<PokemonType> Map()
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

            return pokemonTypes;
        }

        public override void MapAndSave()
        {
            List<PokemonType> pokemonTypes = Map();

            _dbContext.Types.AddRange(pokemonTypes);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemonTypes.Count} types");
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }
    }
}
