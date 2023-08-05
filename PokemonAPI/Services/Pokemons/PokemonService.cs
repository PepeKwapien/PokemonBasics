using Models.Pokemons;
using PokemonAPI.DTOs;
using PokemonAPI.Repositories;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public Pokemon GetPokemonByName(string name)
        {
            return _pokemonRepository.GetByName(name);
        }

        public PokemonSearchItemDto[] GetPokemonsSearchItemsWithSimilarNames(string name, int levenshteinDistance = 3)
        {
            return _pokemonRepository
                .GetPokemonsSimilarNames(name, levenshteinDistance)
                .Select(pokemon => PokemonSearchItemDto.FromPokemon(pokemon))
                .ToArray();
        }
    }
}
