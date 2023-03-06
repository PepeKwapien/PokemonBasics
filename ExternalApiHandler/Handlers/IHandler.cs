using ExternalApiHandler.DTOs;

namespace ExternalApiHandler.Handlers
{
    internal interface IHandler<T> where T : IDto
    {
        void Handle(T[] values);
    }
}
