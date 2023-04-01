using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler.Requesters
{
    internal class PokeballsRequester : IPokeballsRequester
    {
        private readonly IHttpClientFactory _externalHttpClientFactory;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokeballsRequester(
            IHttpClientFactory httpClientFactory,
            IOptions<ExternalApiOptions> externalApiOptions)
        {
            _externalHttpClientFactory = httpClientFactory;
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
                    var pokeballCategory = await RequesterHelper.Get<ItemCategoryDto>(client, path);
                    pokeballUrls.AddRange(pokeballCategory.items.Select(item => item.url));
                }

                pokeballs = await RequesterHelper.GetCollection<PokeballDto>(client, pokeballUrls, _externalApiOptions.BaseUrl);
            }

            return pokeballs;
        }
    }
}
