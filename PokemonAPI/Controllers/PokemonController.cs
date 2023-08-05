using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DTO;
using PokemonAPI.Repositories;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonRepository pokemonRepository, ILogger<PokemonController> logger)
        {
            _pokemonRepository = pokemonRepository;
            _logger = logger;
        }

        [Route("{pokemonName}")]
        [HttpGet]
        public PokemonSearchItemDto[] GetSimilarNames(string pokemonName)
        {
            return _pokemonRepository
                .GetPokemonsSearchItemsWithSimilarNames(pokemonName)
                .ToArray();
        }
    }
}