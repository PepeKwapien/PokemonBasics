using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
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

        public override List<PokemonEntry> MapToDb()
        {
            List<PokemonEntry> pokemonEntries = new List<PokemonEntry>();

            foreach (PokedexDto pokedexDto in _pokedexDtos)
            {
                Pokedex pokedex = EntityFinderHelper.FindEntityByDtoName(_dbContext.Pokedexes, pokedexDto.name, _pokedexDtos, _logger);

                foreach (var pokemonEntry in pokedexDto.pokemon_entries)
                {
                    PokemonSpeciesDto matchingSpecies = _pokemonSpeciesDtos.FirstOrDefault(dto => dto.name.Equals(pokemonEntry.pokemon_species.name));

                    foreach (var variety in matchingSpecies.varieties)
                    {
                        Pokemon? pokemon = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, variety.pokemon.name, _logger);
                        if(pokemon == null)
                        {
                            _logger.Warn($"No pokemon with name {variety.pokemon.name} was found. Skipping creating its entry");
                            continue;
                        }

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
