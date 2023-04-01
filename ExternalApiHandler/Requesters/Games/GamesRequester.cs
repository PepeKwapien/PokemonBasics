using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class GamesRequester : IGamesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public GamesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<GamesDto>> GetCollection()
        {
            List<GamesDto> games = new List<GamesDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {

                List<VersionGroupDto> versionGroups = await RequesterHelper.GetCollectionFromRestfulPoint<VersionGroupDto>(client, _externalApiOptions.VersionGroupPath, _externalApiOptions.BaseUrl);

                foreach (var versionGroup in versionGroups)
                {
                    List<string> versionCollection = versionGroup.versions.Select(version => version.url).ToList();
                    List<VersionDto> versions = await RequesterHelper.GetCollection<VersionDto>(client , versionCollection, _externalApiOptions.BaseUrl);

                    games.Add(new GamesDto { VersionGroup= versionGroup, Versions = versions });
                }
            }

            return games;
        }
    }
}
