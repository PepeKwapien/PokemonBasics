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
            string? next = path;

            do
            {
                Console.WriteLine(next);
                CountDto count = await Get<CountDto>(client, next);
                collectionUrls.AddRange(count.results.Select(result => result.url.Replace(parentUrl, "")));
                next = count.next;
            } while (!String.IsNullOrEmpty(next));

            return collectionUrls;
        }

        public static async Task<List<T>> GetCollection<T>(HttpClient client, string path, string parentUrl)
        {
            List<T> collection = new List<T>();
            List<string> collectionUrls = await RequesterHelper.GetCollectionUrls(client, path, parentUrl);

            foreach (var url in collectionUrls)
            {
                Console.WriteLine(url);
                T dto = await RequesterHelper.Get<T>(client, url);
                collection.Add(dto);
            }

            return collection;
        }
    }
}
