using ExternalApiHandler.DTOs;
using Logger;
using System.Text.Json;

namespace ExternalApiHandler.Helpers
{
    internal class RequesterHelper
    {
        // Method to get dto object from given endpoint and deserialize it
        public static async Task<T> Get<T>(HttpClient client, string path, ILogger? logger = null)
        {
            var result = await client.GetAsync(path);
            var stringifiedContent = await result.Content.ReadAsStringAsync();
            T dto = JsonSerializer.Deserialize<T>(stringifiedContent);

            logger?.Debug($"Object of type {typeof(T)} receiver from endpoint {path}");

            return dto;
        }

        // Method to get Urls of a certain collection from restful Api
        public static async Task<List<string>> GetCollectionUrls(HttpClient client, string path, string urlMask, ILogger? logger = null)
        {
            List<string> collectionUrls = new List<string>();
            string? next = path;

            do
            {
                logger?.Info($"Reaching endpoint {next} to get Urls of objects");
                CountDto count = await Get<CountDto>(client, next, logger);
                collectionUrls.AddRange(count.results.Select(result => result.url.Replace(urlMask, "")));
                next = count.next;
            } while (!String.IsNullOrEmpty(next));

            logger?.Debug($"Received collection of {collectionUrls.Count} Urls");

            return collectionUrls;
        }

        // Method to get collection of objects using collection of Urls
        public static async Task<List<T>> GetCollection<T>(HttpClient client, ICollection<string> collectionUrls, string? urlMask = null, ILogger? logger = null)
        {
            List<T> collection = new List<T>();

            foreach (var url in collectionUrls)
            {
                string getUrl = urlMask != null ? url.Replace(urlMask, "") : url;

                logger?.Info($"Reaching endpoint {getUrl} to get object of type {typeof(T)}");

                T dto = await Get<T>(client, getUrl, logger);
                collection.Add(dto);
            }

            logger?.Debug($"Received collection of {collection.Count} objects of type {typeof(T)}");

            return collection;
        }

        // Method to get collection Urls from restful Api and then get these objects
        public static async Task<List<T>> GetCollectionFromRestfulPoint<T>(HttpClient client, string path, string urlMask, ILogger? logger = null)
        {
            List<string> collectionUrls = await GetCollectionUrls(client, path, urlMask, logger);

            return await GetCollection<T>(client, collectionUrls, logger: logger);
        }
    }
}
