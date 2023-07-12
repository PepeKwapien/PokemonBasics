
using DataAccess;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.Extensions.Options;
using Models.Pokeballs;

namespace ExternalApiCrawler.ImageManagerNS
{
    public class ImageManager : IImageManager
    {
        private readonly IPokemonDatabaseContext _databaseContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly ImageOptions _options;

        public ImageManager(IPokemonDatabaseContext databaseContext, IHttpClientFactory httpClientFactory, IOptions<ImageOptions> options, ILogger logger)
        {
            _databaseContext = databaseContext;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = options.Value;
        }
        private async Task CopyAndUploadPokemonImages()
        {
            foreach(var pokemon in _databaseContext.Pokemons)
            {
                if(pokemon.Sprite == null)
                {
                    continue;
                }

                using (var client = _httpClientFactory.CreateClient())
                {
                    string filePath = $"{_options.ImagesLocation}/Pokemons/{pokemon.Name}.png";
                    await RequesterHelper.DownloadImage(client, pokemon.Sprite, filePath, _logger);
                    string link = await RequesterHelper.UploadImage(client, _options.ImgurUrl, filePath, _options.AccessToken, _logger);
                    pokemon.Sprite = String.IsNullOrEmpty(link) ? pokemon.Sprite : link;
                }
            }

            _databaseContext.SaveChanges();
        }

        private async Task CopyAndUploadPokeballImages()
        {
            foreach (var pokeball in _databaseContext.Pokeballs)
            {
                if (pokeball.Sprite == null)
                {
                    continue;
                }

                using (var client = _httpClientFactory.CreateClient())
                {
                    string filePath = $"{_options.ImagesLocation}/Pokeballs/{pokeball.Name}.png";
                    await RequesterHelper.DownloadImage(client, pokeball.Sprite, filePath, _logger);
                    string link = await RequesterHelper.UploadImage(client, _options.ImgurUrl, filePath, _options.AccessToken, _logger);
                    
                    pokeball.Sprite = String.IsNullOrEmpty(link) ? pokeball.Sprite : link;
                }
            }

            _databaseContext.SaveChanges();
        }

        public async Task Run()
        {
            if (_options.DownloadImages)
            {
                await CopyAndUploadPokemonImages();
                await CopyAndUploadPokeballImages();
            }
        }
    }
}
