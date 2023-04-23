using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;

namespace ExternalApiCrawler.Requesters
{
    internal class PokeballsRequester : IPokeballsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ILogger _logger;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokeballsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions,
            ILogger logger
            )
        {
            _externalHttpClientFactory = httpClientFactory;
            _logger = logger;
            _externalApiOptions = externalApiOptions.Value;
        }

        public async Task<List<PokeballDto>> GetCollection()
        {
            List<PokeballDto> pokeballs = new List<PokeballDto>();
            List<string> pokeballUrls = new List<string>();

            using (var client = _externalHttpClientFactory.CreateClient(_externalApiOptions.ClientName))
            {
                
                foreach(string itemCategory in _externalApiOptions.ItemCategoriesForPokeballs)
                {
                    string path = $"{_externalApiOptions.ItemCategoryPath}/{itemCategory}";
                    var pokeballCategory = await RequesterHelper.Get<ItemCategoryDto>(client, path, _logger);
                    pokeballUrls.AddRange(pokeballCategory.items.Select(item => item.url));
                }

                pokeballs = await RequesterHelper.GetCollection<PokeballDto>(client, pokeballUrls, _logger);
            }

            return pokeballs;
        }
    }
}
