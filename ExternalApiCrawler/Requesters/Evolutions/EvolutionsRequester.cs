﻿using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
{
    public class EvolutionsRequester : IEvolutionsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public EvolutionsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<EvolutionChainDto>> GetCollection()
        {
            List<EvolutionChainDto> evolutions = new List<EvolutionChainDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                evolutions = await RequesterHelper.GetCollectionFromRestfulPoint<EvolutionChainDto>(client, _externalApiOptions.EvolutionChainPath, _logger);
            }

            return evolutions;
        }
    }
}
