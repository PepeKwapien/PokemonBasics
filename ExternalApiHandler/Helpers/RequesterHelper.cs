using System.Text.Json;

namespace ExternalApiHandler.Helpers
{
    internal class RequesterHelper
    {
        public static async Task<T> Get<T>(HttpClient client, string path)
        {
            var result = await client.GetAsync(path);
            var stringifiedContent = await result.Content.ReadAsStringAsync();
            T dto = JsonSerializer.Deserialize<T>(stringifiedContent);

            return dto;
        }
    }
}
