
using DataAccess;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.Pokeballs;
using Models.Pokemons;

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

        public async Task Run()
        {
            await RunPokemons();
            await RunPokeballs();
        }

        private async Task DownloadImages<T>(DbSet<T> collection, string directoryPath) where T : class, IHasSprite, IHasName
        {
            foreach (var entity in collection)
            {
                if (entity.Sprite == null)
                {
                    continue;
                }

                using (var client = _httpClientFactory.CreateClient())
                {
                    await RequesterHelper.DownloadImage(client, entity.Sprite, CreateFilePath(directoryPath, entity.Name), _logger);
                }
            }

            _databaseContext.SaveChanges();
        }

        private async Task UploadImages<T>(DbSet<T> collection, string directoryPath) where T : class, IHasSprite, IHasName
        {
            foreach (var entity in collection)
            {
                if (entity.Sprite == null)
                {
                    continue;
                }

                using (var client = _httpClientFactory.CreateClient())
                {
                    string link = await RequesterHelper.UploadImage(client, _options.ImgurUrl, CreateFilePath(directoryPath, entity.Name),
                        CreateAuthHeader(_options.AccessToken, _options.ClientId), _logger);
                    entity.Sprite = String.IsNullOrEmpty(link) ? entity.Sprite : link;
                }
            }

            _databaseContext.SaveChanges();
        }

        private async Task RunPokemons()
        {
            string directoryPath = $"{_options.ImagesLocation}{_options.PokemonImagesLocation}";
            DbSet<Pokemon> pokemons = _databaseContext.Pokemons;

            if (_options.DownloadImages)
            {
                await DownloadImages<Pokemon>(pokemons, directoryPath);
            }

            if (_options.UploadImages)
            {
                await UploadImages<Pokemon>(pokemons, directoryPath);
            }
        }

        private async Task RunPokeballs()
        {
            string directoryPath = $"{_options.ImagesLocation}{_options.PokeballImagesLocation}";
            DbSet<Pokeball> pokeballs = _databaseContext.Pokeballs;

            if (_options.DownloadImages)
            {
                await DownloadImages<Pokeball>(pokeballs, directoryPath);
            }

            if (_options.UploadImages)
            {
                await UploadImages<Pokeball>(pokeballs, directoryPath);
            }
        }
        
        private string CreateFilePath(string directoryPath, string fileName)
        {
            return $"{directoryPath}/{fileName}.png";
        }

        private string CreateAuthHeader(string accessToken, string clientId)
        {
            return String.IsNullOrEmpty(accessToken) ? $"Client-ID {clientId}" : $"Bearer {accessToken}";
        }
    }
}
