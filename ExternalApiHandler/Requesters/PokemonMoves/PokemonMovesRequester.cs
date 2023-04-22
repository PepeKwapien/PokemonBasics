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

        public async Task<List<MoveDto>> GetCollection()
        {
            List<MoveDto> pokemonMoves = new List<MoveDto>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                pokemonMoves = await RequesterHelper.GetCollectionFromRestfulPoint<MoveDto>(client, _externalApiOptions.PokemonMovePath, _logger);
            }

            return pokemonMoves;
        }
    }
}
