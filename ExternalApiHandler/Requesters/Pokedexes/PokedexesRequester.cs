﻿using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
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
                pokedexes = await RequesterHelper.GetCollectionFromRestfulPoint<PokedexDto>(client, _externalApiOptions.PokedexPath, _externalApiOptions.BaseUrl, _logger);
            }

            return pokedexes;
        }
    }
}
