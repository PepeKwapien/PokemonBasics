using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
{
    internal class PokemonTypesRequester : IPokemonTypesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonTypesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonTypeDto>> GetCollection()
        {
            List<PokemonTypeDto> pokemonTypes = new List<PokemonTypeDto>();

            using(var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonTypes = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonTypeDto>(client, _externalApiOptions.PokemonTypePath, _logger);
            }

            return pokemonTypes;
        }
    }
}
