﻿using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class GamesRequester : IGamesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public GamesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<GamesDto>> GetCollection()
        {
            List<GamesDto> games = new List<GamesDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {

                List<VersionGroupDto> versionGroups = await RequesterHelper.GetCollectionFromRestfulPoint<VersionGroupDto>
                    (client, _externalApiOptions.VersionGroupPath, _externalApiOptions.BaseUrl, _logger);

                foreach (var versionGroup in versionGroups)
                {
                    List<string> versionCollection = versionGroup.versions.Select(version => version.url).ToList();
                    List<VersionDto> versions = await RequesterHelper.GetCollection<VersionDto>(client ,versionCollection, _externalApiOptions.BaseUrl, _logger);

                    games.Add(new GamesDto { VersionGroup= versionGroup, Versions = versions });
                }
            }

            return games;
        }
    }
}
