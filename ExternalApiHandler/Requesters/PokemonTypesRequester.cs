using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonTypesRequester : IRequester<PokemonTypeDto>
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
                for (int i = 1; i <= _externalApiOptions.NumberOfPokemonTypes; i++)
                {
                    string path = $"{_externalApiOptions.PokemonTypePath}/{i}";
                    PokemonTypeDto typeDto = await RequesterHelper.Get<PokemonTypeDto>(client, path);
                    pokemonTypes.Add(typeDto);
                }
            }

            Console.WriteLine(pokemonTypes.Count);

            return pokemonTypes;
        }
    }
}
