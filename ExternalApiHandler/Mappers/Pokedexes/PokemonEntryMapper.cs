using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Pokedexes;
using Models.Pokemons;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonEntryMapper : Mapper<PokemonEntry>
    {
        private readonly ILogger _logger;
        private List<PokedexDto> _pokedexDtos;
        private List<PokemonSpeciesDto> _pokemonSpeciesDtos;

        public PokemonEntryMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokedexDtos = new List<PokedexDto>();
            _pokemonSpeciesDtos = new List<PokemonSpeciesDto>();
        }

        public override List<PokemonEntry> Map()
        {
            List<PokemonEntry> pokemonEntries = new List<PokemonEntry>();

            foreach(PokedexDto pokedexDto in _pokedexDtos)
            {
                Pokedex pokedex = EntityFinderHelper.FindEntityByDtoName(_dbContext.Pokedexes, pokedexDto.name, _pokedexDtos, _logger);

                foreach (var pokemonEntry in pokedexDto.pokemon_entries)
                {
                    PokemonSpeciesDto matchingSpecies = _pokemonSpeciesDtos.FirstOrDefault(dto => dto.name.Equals(pokemonEntry.pokemon_species.name));

                    foreach (var variety in matchingSpecies.varieties)
                    {
                        Pokemon pokemon = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, variety.pokemon.name, _logger);

                        pokemonEntries.Add(new PokemonEntry
                        {
                            PokemonId = pokemon.Id,
                            Pokemon = pokemon,
                            PokedexId = pokedex.Id,
                            Pokedex = pokedex,
                            Number = pokemonEntry.entry_number
                        });
                        _logger.Debug($"Mapped pokemon {pokemon.Name} entry in a {pokedex.Name} pokedex");
                    } // End loop through varieties
                } // End loop through pokemon entries
            } // End loop through pokedex dtos

            foreach (PokemonEntry pokemonEntry in _dbContext.PokemonEntries.Include(pe => pe.Pokemon).Include(pe => pe.Pokedex))
            {
                _logger.Debug($"Removing pokemon {pokemonEntry.Pokemon.Name} entry in pokedex {pokemonEntry.Pokedex.Name}");
                _dbContext.PokemonEntries.Remove(pokemonEntry);
            }

            _dbContext.PokemonEntries.AddRange(pokemonEntries);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemonEntries.Count} pokemon entries");

            return pokemonEntries;
        }

        public void SetUp(List<PokedexDto> pokedexes, List<PokemonSpeciesDto> pokemonSpecies)
        {
            _pokedexDtos = pokedexes;
            _pokemonSpeciesDtos = pokemonSpecies;
        }
    }
}
