﻿using DataAccess;
using ExternalApiCrawler.DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Games;
using Models.Pokemons;
using Models.Types;

namespace ExternalApiCrawler.Helpers
{
    public class EntityFinderHelper
    {
        public static PokemonType FindTypeByNameCaseInsensitive(DbSet<PokemonType> typeSet, string typeName)
        {
            return typeSet.FirstOrDefault(type => type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        public static T FindEntityByDtoName<T, TDto>(DbSet<T> dbSet, string simpleName, List<TDto> dtos) where T : class, IModel, IHasName where TDto : class, IDto, IMultiLanguageNames
        {
            TDto dtoWithMatchingName = dtos.FirstOrDefault(dto => dto.name.Equals(simpleName, StringComparison.OrdinalIgnoreCase));
            string databaseName = LanguageVersionHelper.FindEnglishVersion(dtoWithMatchingName.names).name;

            return dbSet.FirstOrDefault(entity => entity.Name.Equals(databaseName));
        }

        public static Pokemon FindPokemonByName(DbSet<Pokemon> pokemonSet, string pokemonName)
        {
            return pokemonSet.FirstOrDefault(pokemon => pokemon.Name.Equals(StringHelper.Normalize(pokemonName)));
        }

        public static List<Game> FindGamesByVersionGroupName(DbSet<Game> gamesSet, string versionGroupName, List<GamesDto> gamesDtos)
        {
            List<Game> games = new List<Game>();

            GamesDto gamesDto = gamesDtos.FirstOrDefault(gamesDto => gamesDto.VersionGroup.name.Equals(versionGroupName));

            if (gamesDto != null)
            {
                foreach(var version in gamesDto.Versions)
                {
                    Game game = FindEntityByDtoName(gamesSet, version.name, gamesDto.Versions);
                    games.Add(game);
                }
            }

            return games;
        }

        public static List<Pokemon> FindVarietiesInPokemonSpecies(DbSet<Pokemon> pokemonSet, string speciesName, List<PokemonSpeciesDto> pokemonSpeciesDtos)
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            PokemonSpeciesDto matchingSpecies = pokemonSpeciesDtos.FirstOrDefault(psd => psd.name == speciesName);

            foreach (var variety in matchingSpecies.varieties)
            {
                pokemons.Add(FindPokemonByName(pokemonSet, variety.pokemon.name));
            }

            return pokemons;
        }
    }
}
