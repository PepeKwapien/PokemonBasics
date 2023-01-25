using Microsoft.Extensions.Configuration;

namespace ExternalApiHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            ExternalApiSettings settings = config.GetRequiredSection("ExternalApiSettings").Get<ExternalApiSettings>();

            Console.WriteLine(settings.BaseUrl);
        }
    }
}