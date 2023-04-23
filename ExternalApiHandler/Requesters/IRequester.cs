using ExternalApiCrawler.DTOs;

namespace ExternalApiCrawler.Requesters
{
    internal interface IRequester<T> where T: IDto
    {
        Task<List<T>> GetCollection();
    }
}
