using ExternalApiCrawler.Requesters;
using ExternalApiCrawler.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExternalApiCrawler.Mappers;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Logger;
using ExternalApiCrawler.Helpers;

namespace ExternalApiCrawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Set up configuration builder
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("Config/appsettings.json")
               .Build();

            // Bind class with options for http client
            ExternalApiOptions externalOptions = new ExternalApiOptions();
            config.GetRequiredSection("ExternalApiSettings").Bind(externalOptions);

            // Options
            var serviceCollection = new ServiceCollection()
                .Configure<ExternalApiOptions>(config.GetRequiredSection("ExternalApiSettings"))
                .Configure<LoggerOptions>(config.GetRequiredSection("LoggerSettings"));

            // DbContext
            serviceCollection
                .AddDbContext<IPokemonDatabaseContext, PokemonDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultDatabase")));

            // Http client
            serviceCollection.AddHttpClient(externalOptions.ClientName, client =>
            {
                client.BaseAddress = new Uri(externalOptions.BaseUrl);
            });

            // Logger
            serviceCollection
                .AddScoped<ILogger>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<LoggerOptions>>();
                var optionsValue = options.Value;
                return new FileLogger(optionsValue.FilePath, optionsValue.FileName, optionsValue.LoggerLevel);
            });

            // Requesters
            serviceCollection
                .AddScoped<IPokemonTypesRequester, PokemonTypesRequester>()
                .AddScoped<IPokemonAbilitiesRequester, PokemonAbilitiesRequester>()
                .AddScoped<IPokemonMovesRequester, PokemonMovesRequester>()
                .AddScoped<IGenerationsRequester, GenerationsRequester>()
                .AddScoped<IPokeballsRequester, PokeballsRequester>()
                .AddScoped<IGamesRequester, GamesRequester>()
                .AddScoped<IPokedexesRequester, PokedexesRequester>()
                .AddScoped<IPokemonsRequester, PokemonsRequester>()
                .AddScoped<IPokemonSpeciesRequester, PokemonSpeciesRequester>()
                .AddScoped<IEvolutionsRequester, EvolutionsRequester>();

            // Mappers
            serviceCollection
                .AddScoped<AbilityMapper>()
                .AddScoped<PokemonAbilityMapper>()
                .AddScoped<GameMapper>()
                .AddScoped<GenerationMapper>()
                .AddScoped<MoveMapper>()
                .AddScoped<PokemonMoveMapper>()
                .AddScoped<PokeballMapper>()
                .AddScoped<PokedexMapper>()
                .AddScoped<PokemonEntryMapper>()
                .AddScoped<EvolutionMapper>()
                .AddScoped<PokemonMapper>()
                .AddScoped<AlternateFormMapper>()
                .AddScoped<DamageMultiplierMapper>()
                .AddScoped<PokemonTypeMapper>();

            // Orchestrator
            serviceCollection.AddScoped<IOrchestrator, Orchestrator>();
            
            // Build service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                /* var orchestrator = scope.ServiceProvider.GetService<IOrchestrator>();
                 bool result = await orchestrator.Start();
                 Console.WriteLine($"{(result ? "Yay" : "Nay")}");*/
                var logger = scope.ServiceProvider.GetService<ILogger>();
                var orchestrator = scope.ServiceProvider.GetService<IHttpClientFactory>();
                var client = orchestrator.CreateClient("External");
                await RequesterHelper.DownloadImage(client, "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/132.png", "../../../../Images/ditto.png", logger);
                string link = await RequesterHelper.UploadImage(client, "https://api.imgur.com/3/image", "../../../../Images/ditto.png", "8861f27667ef1b30c1b9ddea793fdeb783e1ca7e", logger);
                Console.WriteLine(link);
            }
        }
    }
}