using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class EvolutionsRequester : IEvolutionsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public EvolutionsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<EvolutionChainDto>> GetCollection()
        {
            List<EvolutionChainDto> evolutions = new List<EvolutionChainDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                evolutions = await RequesterHelper.GetCollectionFromRestfulPoint<EvolutionChainDto>(client, _externalApiOptions.EvolutionChainPath, _externalApiOptions.BaseUrl);
            }

            return evolutions;
        }
    }
}
