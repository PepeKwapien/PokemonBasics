using Microsoft.AspNetCore.Mvc;
using Models.Pokemons;
using PokemonAPI.DTOs;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IPokemonTypeService _pokemonTypeService;
        private readonly IAbilityService _abilityService;

        public PokemonController(IPokemonService pokemonService, IPokemonTypeService pokemonTypeService, IAbilityService abilityService)
        {
            _pokemonService = pokemonService;
            _pokemonTypeService = pokemonTypeService;
            _abilityService = abilityService;
        }

        [Route("search/{pokemonName}")]
        [HttpGet]
        public PokemonSearchItemDto[] GetSimilarNames(string pokemonName)
        {
            return _pokemonService
                .GetPokemonsSearchItemsWithSimilarNames(pokemonName)
                .ToArray();
        }

        [Route("{pokemonName}/general")]
        [HttpGet]
        public IActionResult GetGeneralInformation(string pokemonName)
        {
            Pokemon pokemon = _pokemonService.GetPokemonByName(pokemonName);

            if(pokemon == null)
            {
                return new NotFoundResult();
            }

            string primaryTypeName = pokemon.PrimaryType.Name;
            string? secondaryTypeName = pokemon.SecondaryType?.Name;

            List<AbilityDto> abilities = _abilityService.GetAbilitiesDtoForPokemon(pokemon.Name);
            PokemonDefensiveCharacteristics defensiveRelations = _pokemonTypeService.GetDefensiveCharacteristics(primaryTypeName, secondaryTypeName);
            PokemonTypeCharacteristics primaryTypeCharacteristic = _pokemonTypeService.GetTypeCharacteristic(primaryTypeName);
            PokemonTypeCharacteristics? secondaryTypeCharacteristic = string.IsNullOrEmpty(secondaryTypeName) ? null : _pokemonTypeService.GetTypeCharacteristic(secondaryTypeName);

            return Ok(PokemonGeneralDto.FromPokemonAbilitiesAndTypes(pokemon, abilities, defensiveRelations, primaryTypeCharacteristic, secondaryTypeCharacteristic));
        }
    }
}