using ExternalApiHandler.DTOs;

namespace ExternalApiHandler.Requesters
{
    internal interface IRequester<T> where T: IDto
    {
        Task<List<T>> GetCollection();
    }
}
