using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonMovesRequester : IPokemonMovesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonMovesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonMoveDto>> GetCollection()
        {
            List<PokemonMoveDto> pokemonMoves = new List<PokemonMoveDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonMoves = await RequesterHelper.GetCollectionFromRestfulPoint<PokemonMoveDto>(client, _externalApiOptions.PokemonMovePath, _externalApiOptions.BaseUrl, _logger);
            }

            return pokemonMoves;
        }
    }
}
