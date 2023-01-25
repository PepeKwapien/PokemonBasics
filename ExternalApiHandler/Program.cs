using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExternalApiHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<ExternalApiSettings>(config.GetRequiredSection("ExternalApiSettings"))
                .AddDbContext<PokemonDatabaseContext>()
                .AddHttpClient()
                .BuildServiceProvider();

            using(var scope = serviceProvider.CreateScope())
            {
                var settings = scope.ServiceProvider.GetService<IOptions<ExternalApiSettings>>().Value;

                Console.WriteLine(settings.BaseUrl);
            }
        }
    }
}