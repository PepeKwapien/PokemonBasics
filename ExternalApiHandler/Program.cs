﻿using DataAccess;
using ExternalApiHandler.Requesters;
using ExternalApiHandler.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ExternalApiHandler.Handlers;
using ExternalApiHandler.Requesters.PokemonTypes;

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

            // Handlers
            serviceCollection
                .AddSingleton<PokemonTypeHandler>()
                .AddSingleton<DamageMultiplierHandler>();

            // Requesters
            serviceCollection
                .AddSingleton<IPokemonTypesRequester, PokemonTypesRequester>();

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

                await requester.GetCollection();
            }
        }
    }
}