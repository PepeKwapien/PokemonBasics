using ExternalApiHandler.DTOs;
using ExternalApiHandler.Handlers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ExternalApiHandler.Requesters
{
    internal class PokemonTypesRequester : IRequester<PokemonTypeDto>
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly TypesOptions _options;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonTypesRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            IOptions<TypesOptions> pokemonTypeOptions
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _options = pokemonTypeOptions.Value;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokemonTypeDto>> GetCollection()
        {
            List<PokemonTypeDto> pokemonTypes = new List<PokemonTypeDto>();

            using(var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                for (int i = 1; i <= _options.NoOfTypes; i++)
                {
                    var result = await client.GetAsync($"{_options.Path}/{i}");
                    var stringifiedContent = await result.Content.ReadAsStringAsync();
                    PokemonTypeDto typeDto = JsonSerializer.Deserialize<PokemonTypeDto>(stringifiedContent);
                    pokemonTypes.Add(typeDto);
                }
            }

            Console.WriteLine(pokemonTypes.Count);

            return pokemonTypes;
        }
    }
}
