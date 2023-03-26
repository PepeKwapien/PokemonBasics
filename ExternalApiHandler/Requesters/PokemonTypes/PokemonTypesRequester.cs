using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using ExternalApiHandler.Requesters.PokemonTypes;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonTypesRequester : IPokemonTypesRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonTypesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonTypeDto>> GetCollection()
        {
            List<PokemonTypeDto> pokemonTypes = new List<PokemonTypeDto>();

            using(var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                List<string> collectionUrls = await RequesterHelper.GetCollectionUrls(client, _externalApiOptions.PokemonTypePath, _externalApiOptions.BaseUrl);

                foreach (var url in collectionUrls)
                {
                    PokemonTypeDto typeDto = await RequesterHelper.Get<PokemonTypeDto>(client, url);
                    pokemonTypes.Add(typeDto);
                }
            }

            Console.WriteLine(pokemonTypes.Count);

            return pokemonTypes;
        }
    }
}
