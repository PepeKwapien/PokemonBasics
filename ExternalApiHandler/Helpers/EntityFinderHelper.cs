using DataAccess;
using ExternalApiCrawler.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Generations;
using Models.Types;

namespace ExternalApiCrawler.Helpers
{
    public class EntityFinderHelper
    {
        public static PokemonType FindTypeByNameCaseInsensitive(IPokemonDatabaseContext dbContext, string typeName)
        {
            return dbContext.Types.FirstOrDefault(type => type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        public static T FindEntityByDtoName<T, TDto>(DbSet<T> dbSet, string simpleName, List<TDto> dtos) where T : class, IModel, IHasName where TDto : class, IDto, IMultiLanguageNames
        {
            TDto dtoWithMatchingName = dtos.FirstOrDefault(dto => dto.name.Equals(simpleName, StringComparison.OrdinalIgnoreCase));
            string databaseName = LanguageVersionHelper.FindEnglishVersion(dtoWithMatchingName.names).name;

            return dbSet.FirstOrDefault(entity => entity.Name.Equals(databaseName));
        }
    }
}
