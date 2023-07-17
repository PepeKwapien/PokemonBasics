namespace ExternalApiCrawler
{
    public interface IOrchestrator
    {
        Task<bool> Start();
    }
}
