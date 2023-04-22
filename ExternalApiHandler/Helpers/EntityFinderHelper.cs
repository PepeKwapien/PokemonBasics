using DataAccess;
using ExternalApiHandler.DTOs;
using Models.Generations;
using Models.Types;

namespace ExternalApiHandler.Helpers
{
    public class EntityFinderHelper
    {
        public static PokemonType FindTypeByNameCaseInsensitive(IPokemonDatabaseContext dbContext, string typeName)
        {
            return dbContext.Types.FirstOrDefault(type => type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        public static Generation FindGenerationByDtoName(IPokemonDatabaseContext dbContext, string generationSimpleName, List<GenerationDto> generationDtos)
        {
            GenerationDto generationDtoWithMatchingName = generationDtos.FirstOrDefault(generationDto =>
                generationDto.name.Equals(generationSimpleName, StringComparison.OrdinalIgnoreCase));
            string generationDatabaseName = LanguageVersionHelper.FindEnglishVersion(generationDtoWithMatchingName.names).name;

            return dbContext.Generations.FirstOrDefault(generation => generation.Name.Equals(generationDatabaseName));
        }
    }
}
