using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters.PokemonAbilities
{
    internal class PokemonAbilitiesRequester : IPokemonAbilitiesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonAbilitiesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonAbilityDto>> GetCollection()
        {
            List<PokemonAbilityDto> pokemonAbilities = new List<PokemonAbilityDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonAbilities = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonAbilityDto>(client, _externalApiOptions.PokemonAbilityPath, _externalApiOptions.BaseUrl);
            }

            return pokemonAbilities;
        }
    }
}
