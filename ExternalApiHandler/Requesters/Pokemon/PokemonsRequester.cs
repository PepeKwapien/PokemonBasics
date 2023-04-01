using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonsRequester : IPokemonsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonDto>> GetCollection()
        {
            List<PokemonDto> pokemons = new List<PokemonDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemons = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonDto>(client, _externalApiOptions.PokemonPath, _externalApiOptions.BaseUrl);
            }

            return pokemons;
        }
    }
}
