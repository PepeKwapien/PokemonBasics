using Microsoft.AspNetCore.Mvc;
using PokemonAPI.DTOs;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonService pokemonService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [Route("{pokemonName}")]
        [HttpGet]
        public PokemonSearchItemDto[] GetSimilarNames(string pokemonName)
        {
            return _pokemonService
                .GetPokemonsSearchItemsWithSimilarNames(pokemonName)
                .ToArray();
        }
    }
}