using Azure;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Options;
using Logger;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ExternalApiCrawler.Helpers
{
    public class RequesterHelper
    {
        // Method to get dto object from given endpoint and deserialize it
        public static async Task<T> Get<T>(HttpClient client, string path, ILogger? logger = null)
        {
            HttpResponseMessage result;
            try
            {
                result = await client.GetAsync(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                logger?.Error($"There was an error while getting object of type {typeof(T).Name} from path {path}. Error: {ex.Message}");
                throw;
            }
            
            var stringifiedContent = await result.Content.ReadAsStringAsync();
            T dto = JsonSerializer.Deserialize<T>(stringifiedContent);

            logger?.Debug($"Object of type {typeof(T)} received from endpoint {path}");

            return dto;
        }

        // Method to get Urls of a certain collection from restful Api
        public static async Task<List<string>> GetCollectionUrls(HttpClient client, string path, ILogger? logger = null)
        {
            List<string> collectionUrls = new List<string>();
            string? next = path;

            do
            {
                logger?.Info($"Reaching endpoint {next} to get Urls of objects");
                CountDto count = await Get<CountDto>(client, next, logger);
                collectionUrls.AddRange(count.results.Select(result => result.url));
                next = count.next;
            } while (!String.IsNullOrEmpty(next));

            logger?.Debug($"Received collection of {collectionUrls.Count} Urls");

            return collectionUrls;
        }

        // Method to get collection of objects using collection of Urls
        public static async Task<List<T>> GetCollection<T>(HttpClient client, ICollection<string> collectionUrls, ILogger? logger = null, ExternalApiOptions? options = null)
        {
            List<T> collection = new List<T>();

            foreach (var url in collectionUrls)
            {
                logger?.Info($"Reaching endpoint {url} to get object of type {typeof(T)}");

                T dto = await Get<T>(client, url, logger);
                collection.Add(dto);
            }

            logger?.Debug($"Received collection of {collection.Count} objects of type {typeof(T)}");

            return collection;
        }

        // Method to get collection Urls from restful Api and then get these objects
        public static async Task<List<T>> GetCollectionFromRestfulPoint<T>(HttpClient client, string path, ILogger? logger = null)
        {
            List<string> collectionUrls = await GetCollectionUrls(client, path, logger);

            return await GetCollection<T>(client, collectionUrls, logger: logger);
        }

        public static async Task DownloadImage(HttpClient client, string url, string filePath, ILogger? logger = null)
        {
            HttpResponseMessage result;
            try
            {
                result = await client.GetAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                logger?.Error($"There was an error while getting image from path {url}. Error: {ex.Message}");
                throw;
            }

            using(var streamedContent = await result.Content.ReadAsStreamAsync())
            using(FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await streamedContent.CopyToAsync(fileStream);
            }

            logger?.Debug($"Image from {url} copied to file {filePath}");
        }

        public static async Task<string> UploadImage(HttpClient client, string url, string filePath, string apiKey, ILogger? logger = null)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            string link = "";

            try
            {
                byte[] imageData = File.ReadAllBytes(filePath);
                HttpResponseMessage httpResponseMessage;

                using (ByteArrayContent content = new ByteArrayContent(imageData))
                using(MultipartFormDataContent formData = new MultipartFormDataContent())
                {
                    formData.Add(content, "image");
                    logger?.Debug($"Uploading image {filePath} to {url}");
                    httpResponseMessage = await client.PostAsync(url, formData);
                    logger?.Debug($"Done uploading image {filePath} to {url}");
                }

                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                ImgurResponse jsonResponse = JsonSerializer.Deserialize<ImgurResponse>(responseContent);
                link = jsonResponse.data.link;
            }
            catch (Exception ex)
            {
                logger?.Error($"There was error uploading the image: {ex.Message}");
            }

            return link;
        }

        public class ImgurResponse
        {
            public DataImgurResponse data { get; set; }
            public class DataImgurResponse
            {
                public string link { get; set; }
            }
        }
    }
}
