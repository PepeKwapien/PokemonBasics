using ExternalApiCrawler.DTOs;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Games;
using Models.Pokemons;
using Models.Types;

namespace ExternalApiCrawler.Helpers
{
    public class EntityFinderHelper
    {
        public static PokemonType FindTypeByNameCaseInsensitive(DbSet<PokemonType> typeSet, string typeName, ILogger? logger = null)
        {
            PokemonType? foundType = typeSet.FirstOrDefault(type => type.Name.ToLower() == typeName.ToLower());

            if(foundType == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"Type {typeName} does not exist in the database", logger);
            }

            return foundType;
        }

        public static T FindEntityByDtoName<T, TDto>(DbSet<T> dbSet, string simpleName, List<TDto> dtos, ILogger? logger = null)
            where T : class, IModel, IHasName
            where TDto : class, IDto, IMultiLanguageNames
        {
            TDto dtoWithMatchingName = dtos.FirstOrDefault(dto => dto.name.ToLower() == simpleName.ToLower());

            if(dtoWithMatchingName == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"No dto of type {typeof(TDto).Name} was found to match name {simpleName}", logger);
            }

            string databaseName = LanguageVersionHelper.FindEnglishVersion(dtoWithMatchingName.names)?.name ?? StringHelper.Normalize(simpleName);
            T foundEntity = dbSet.FirstOrDefault(entity => entity.Name.Equals(databaseName));

            if(foundEntity == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"No entity of type {typeof(T).Name} was found to match name {databaseName}", logger);
            }

            return foundEntity;
        }

        public static Pokemon FindPokemonByName(DbSet<Pokemon> pokemonSet, string pokemonName, ILogger? logger = null)
        {
            string databaseName = StringHelper.Normalize(pokemonName);
            Pokemon foundPokemon = pokemonSet.FirstOrDefault(pokemon => pokemon.Name.Equals(databaseName));

            if(foundPokemon == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"Pokemon with name {databaseName} does not exist in the database", logger);
            }

            return foundPokemon;
        }

        public static List<Game> FindGamesByVersionGroupName(DbSet<Game> gamesSet, string versionGroupName, List<GamesDto> gamesDtos, ILogger? logger = null)
        {
            List<Game> games = new List<Game>();

            GamesDto gamesDto = gamesDtos.FirstOrDefault(gamesDto => gamesDto.VersionGroup.name.Equals(versionGroupName));

            if(gamesDto == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"No game dto was found to match version group name {versionGroupName}", logger);
            }

            foreach (var version in gamesDto.Versions)
            {
                games.Add(FindEntityByDtoName(gamesSet, version.name, gamesDto.Versions, logger));
            }

            return games;
        }

        public static List<Pokemon> FindRegionalFormsInSpecies(
            DbSet<Pokemon> pokemonSet,
            string speciesName,
            List<PokemonSpeciesDto> pokemonSpeciesDtos,
            ILogger? logger = null)
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            PokemonSpeciesDto matchingSpecies = pokemonSpeciesDtos.FirstOrDefault(psd => psd.name == speciesName);

            if(matchingSpecies == null)
            {
                ExceptionHelper.LogAndThrow<Exception>($"No matching pokemon species was found under the name {speciesName}", logger);
            }

            foreach (var variety in matchingSpecies.varieties)
            {
                if (!variety.pokemon.name.Contains("-totem")
                    && !variety.pokemon.name.Contains("-mega")
                    && !variety.pokemon.name.Contains("-gmax")
                    && !variety.pokemon.name.Contains("-eternal")
                    && !variety.pokemon.name.Contains("-roaming")) // Ignore totems, gmax, megas, roaming gimighoul and floette eternal (like what even is this)
                {
                    pokemons.Add(FindPokemonByName(pokemonSet, variety.pokemon.name, logger));
                }
            }

            return pokemons;
        }
    }
}
