using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Models.Types;

namespace ExternalApiHandler.Mappers
{
    internal class PokemonTypeMapper : Mapper<PokemonType>
    {
        private List<PokemonTypeDto> _pokemonTypesDto;

        public PokemonTypeMapper(PokemonDatabaseContext dbContext) : base(dbContext)
        {
           
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
            }

            foreach(var type in _dbContext.Types)
            {
                _dbContext.Types.Remove(type);
            }

            _dbContext.Types.AddRange(pokemonTypes);
            _dbContext.SaveChanges();

            return pokemonTypes;
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }
    }
}
