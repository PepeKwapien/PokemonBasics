using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
{
    internal class PokedexesRequester : IPokedexesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokedexesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokedexDto>> GetCollection()
        {
            List<PokedexDto> pokedexes = new List<PokedexDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokedexes = await RequesterHelper.GetCollectionFromRestfulPoint<PokedexDto>(client, _externalApiOptions.PokedexPath, _logger);
            }

            return pokedexes;
        }
    }
}
