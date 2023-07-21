using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models.Pokemons;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonDatabaseContext _dbContext;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonDatabaseContext _dbContext,ILogger<PokemonController> logger)
        {
            this._dbContext = _dbContext;
            _logger = logger;
        }

        [HttpGet("pikachu", Name = "GetPikachu")]
        public string GetPikachu()
        {
            return _dbContext.Pokemons.FirstOrDefault(pokemon => pokemon.PrimaryType.Name == "Electric").Name ?? "Pikachu ran away :(";
        }

        [Route("{pokemonName}")]
        [HttpGet]
        public string GetPokemon(string pokemonName)
        {
            return _dbContext.Pokemons.FirstOrDefault(pokemon => pokemon.Name.ToLower() == pokemonName.ToLower())?.Name ?? $"{pokemonName} ran away :(";
        }
    }
}