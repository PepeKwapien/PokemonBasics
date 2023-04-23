using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
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

        public async Task<List<AbilityDto>> GetCollection()
        {
            List<AbilityDto> pokemonAbilities = new List<AbilityDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonAbilities = await RequesterHelper.GetCollectionFromRestfulPoint<AbilityDto>(client, _externalApiOptions.PokemonAbilityPath, _logger);
            }

            return pokemonAbilities;
        }
    }
}
