using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonAbilitiesRequester : IPokemonAbilitiesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonAbilitiesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonAbilityDto>> GetCollection()
        {
            List<PokemonAbilityDto> pokemonAbilities = new List<PokemonAbilityDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonAbilities = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonAbilityDto>(client, _externalApiOptions.PokemonAbilityPath, _externalApiOptions.BaseUrl, _logger);
            }

            return pokemonAbilities;
        }
    }
}
