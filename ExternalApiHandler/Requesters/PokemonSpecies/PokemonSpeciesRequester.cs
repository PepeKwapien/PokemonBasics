using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonSpeciesRequester : IPokemonSpeciesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonSpeciesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonSpeciesDto>> GetCollection()
        {
            List<PokemonSpeciesDto> pokemonSpecies = new List<PokemonSpeciesDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonSpecies = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonSpeciesDto>(client, _externalApiOptions.PokemonSpeciesPath, _externalApiOptions.BaseUrl, _logger);
            }

            return pokemonSpecies;
        }
    }
}
