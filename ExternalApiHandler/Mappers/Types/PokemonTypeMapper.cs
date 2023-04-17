using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Types;

namespace ExternalApiHandler.Mappers
{
    internal class PokemonTypeMapper : Mapper<PokemonType>
    {
        private readonly ILogger _logger;
        private List<PokemonTypeDto> _pokemonTypesDto;

        public PokemonTypeMapper(PokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
        }

        public override List<PokemonType> Map()
        {
            List<PokemonType> pokemonTypes = new List<PokemonType>();

            foreach(PokemonTypeDto typeDto in _pokemonTypesDto)
            {
                string name = typeDto.names.FirstOrDefault(name => name.language.name.Equals("en")).name;
                string color = TypeColorHelper.GetTypeColor(typeDto.name);

                PokemonType newType = new PokemonType()
                {
                    Name = name,
                    Color= color,
                };

                pokemonTypes.Add(newType);
                _logger.Debug($"Mapped type {name} with color {color}");
            }

            // Due to annoying foreign key structure I have to remove multipliers manually
            foreach (var damageMultiplier in _dbContext.DamageMultipliers.Include(dm=>dm.Type).Include(dm=>dm.Against))
            {
                _logger.Debug($"Removing damage multiplier when {damageMultiplier.Type.Name} is attacking {damageMultiplier.Against.Name}");
                _dbContext.DamageMultipliers.Remove(damageMultiplier);
            }
            foreach (var type in _dbContext.Types)
            {
                _logger.Debug($"Removing type {type.Name}");
                _dbContext.Types.Remove(type);
            }

            _dbContext.Types.AddRange(pokemonTypes);
            _dbContext.SaveChanges();
            _logger.Success($"Saved {pokemonTypes.Count} types");

            return pokemonTypes;
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }
    }
}
