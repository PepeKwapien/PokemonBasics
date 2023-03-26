using ExternalApiHandler.DTOs;
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

        public static async Task<List<string>> GetCollectionUrls(HttpClient client, string path, string parentUrl)
        {
            List<string> collectionUrls = new List<string>();
            string? next = null;

            do
            {
                CountDto count = await Get<CountDto>(client, path);
                collectionUrls.AddRange(count.results.Select(result => result.url.Replace(parentUrl, "")));
                next = count.next;
            } while (!String.IsNullOrEmpty(next));

            return collectionUrls;
        }
    }
}
