using ExternalApiCrawler.Requesters;
using ExternalApiCrawler.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExternalApiCrawler.Mappers;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Logger;

namespace ExternalApiCrawler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("Config/appsettings.json")
               .Build();

            ExternalApiOptions externalOptions = new ExternalApiOptions();
            config.GetRequiredSection("ExternalApiSettings").Bind(externalOptions);

            // Options
            var serviceCollection = new ServiceCollection()
                .Configure<ExternalApiOptions>(config.GetRequiredSection("ExternalApiSettings"))
                .Configure<LoggerOptions>(config.GetRequiredSection("LoggerSettings"));

            // DbContext
            serviceCollection
                .AddDbContext<IPokemonDatabaseContext, PokemonDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultDatabase")));

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
                .AddScoped<PokemonTypeMapper>()
                .AddScoped<DamageMultiplierMapper>()
                .AddScoped<GenerationMapper>();

            serviceCollection.AddHttpClient(externalOptions.ClientName, client =>
            {
                client.BaseAddress = new Uri(externalOptions.BaseUrl);
            });
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var options = scope.ServiceProvider.GetService<IOptions<ExternalApiOptions>>();

                Console.WriteLine(options.Value.BaseUrl);

                var requester = scope.ServiceProvider.GetService<IGenerationsRequester>();
                var collection = await requester.GetCollection();

                var mapper1 = scope.ServiceProvider.GetService<GenerationMapper>();
                mapper1.SetUp(collection);
                var mapResult1 = mapper1.Map();

                Console.WriteLine(mapResult1.Count);
            }
        }
    }
}