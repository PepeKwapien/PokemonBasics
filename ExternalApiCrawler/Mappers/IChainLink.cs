namespace ExternalApiCrawler.Mappers
{
    public interface IChainLink
    {
        IChainLink? NextChainLink { get; set; }
        IChainLink Next(IChainLink nextChainLink);
        void StartChain();
    }
}
