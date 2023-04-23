using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
{
    internal class GenerationsRequester : IGenerationsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public GenerationsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<GenerationDto>> GetCollection()
        {
            List<GenerationDto> generations = new List<GenerationDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                generations = await RequesterHelper.GetCollectionFromRestfulPoint<GenerationDto>(client, _externalApiOptions.GenerationPath, _logger);
            }

            return generations;
        }
    }
}
