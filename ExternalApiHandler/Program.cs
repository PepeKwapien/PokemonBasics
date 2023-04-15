using ExternalApiHandler.Requesters;
using ExternalApiHandler.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExternalApiHandler.Mappers;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ExternalApiHandler
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
                .Configure<ExternalApiOptions>(config.GetRequiredSection("ExternalApiSettings"));

            // DbContext
            serviceCollection
                .AddDbContext<PokemonDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultDatabase")));

            // Requesters
            serviceCollection
                .AddSingleton<IPokemonTypesRequester, PokemonTypesRequester>()
                .AddSingleton<IPokemonAbilitiesRequester, PokemonAbilitiesRequester>()
                .AddSingleton<IPokemonMovesRequester, PokemonMovesRequester>()
                .AddSingleton<IGenerationsRequester, GenerationsRequester>()
                .AddSingleton<IPokeballsRequester, PokeballsRequester>()
                .AddSingleton<IGamesRequester, GamesRequester>()
                .AddSingleton<IPokedexesRequester, PokedexesRequester>()
                .AddSingleton<IPokemonsRequester, PokemonsRequester>()
                .AddSingleton<IPokemonSpeciesRequester, PokemonSpeciesRequester>()
                .AddSingleton<IEvolutionsRequester, EvolutionsRequester>();

            // Mappers
            serviceCollection
                .AddScoped<PokemonTypeMapper>();

            serviceCollection.AddHttpClient(externalOptions.ClientName, client =>
            {
                client.BaseAddress = new Uri(externalOptions.BaseUrl);
            });
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var options = scope.ServiceProvider.GetService<IOptions<ExternalApiOptions>>();

                Console.WriteLine(options.Value.BaseUrl);

                var requester = scope.ServiceProvider.GetService<IPokemonTypesRequester>();
                var collection = await requester.GetCollection();

                var mapper = scope.ServiceProvider.GetService<PokemonTypeMapper>();
                mapper.SetUp(collection);
                var mapResult = mapper.Map();

                Console.WriteLine(mapResult.Count);
            }
        }
    }
}