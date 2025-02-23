﻿using ExternalApiCrawler.Requesters;
using ExternalApiCrawler.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExternalApiCrawler.Mappers;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Logger;
using ExternalApiCrawler.ImageManagerNS;

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
            ExternalApiOptions externalOptions = new();
            config.GetRequiredSection("ExternalApiSettings").Bind(externalOptions);

            var serviceCollection = new ServiceCollection();

            // Options
            serviceCollection
                .Configure<ExternalApiOptions>(config.GetRequiredSection("ExternalApiSettings"))
                .Configure<LoggerOptions>(config.GetRequiredSection("LoggerSettings"))
                .Configure<ImageOptions>(config.GetRequiredSection("ImageSettings"));

            // DbContext
            serviceCollection
                .AddDbContext<IPokemonDatabaseContext, PokemonDatabaseContext>(options => options.UseNpgsql(config.GetConnectionString("DefaultDatabase")));

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
                .AddScoped<GameVersionMapper>()
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

            // ImageManager
            serviceCollection.AddScoped<IImageManager, ImageManager>();

            // Orchestrator
            serviceCollection.AddScoped<IOrchestrator, Orchestrator>();
            
            // Build service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var orchestrator = scope.ServiceProvider.GetService<IOrchestrator>();
                bool result = await orchestrator.Start();
                Console.WriteLine($"{(result ? "Yay" : "Nay")}");

                /*var imageManager = scope.ServiceProvider.GetService<IImageManager>();
                await imageManager.Run();*/
            }
        }
    }
}