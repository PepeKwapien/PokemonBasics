using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class GenerationsRequester : IGenerationsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public GenerationsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<GenerationDto>> GetCollection()
        {
            List<GenerationDto> generations = new List<GenerationDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                generations = await RequesterHelper.GetCollectionFromRestfulPoint<GenerationDto>(client, _externalApiOptions.GenerationPath, _externalApiOptions.BaseUrl);
            }

            return generations;
        }
    }
}
