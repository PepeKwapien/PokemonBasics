using ExternalApiCrawler.DTOs;

namespace ExternalApiCrawler.Requesters
{
    public interface IRequester<T> where T: IDto
    {
        Task<List<T>> GetCollection();
    }
}
