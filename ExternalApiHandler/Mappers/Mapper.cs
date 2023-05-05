using DataAccess;
using Models;

namespace ExternalApiCrawler.Mappers
{
    public abstract class Mapper<T> : IChainLink where T : class, IModel
    {
        protected readonly IPokemonDatabaseContext _dbContext;

        protected Mapper(IPokemonDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IChainLink? NextChainLink { get; set; }

        // Method to create a chain in a fluent API style
        public IChainLink Next(IChainLink nextChainLink)
        {
            NextChainLink = nextChainLink;

            return nextChainLink;
        }
        public void StartChain()
        {
            MapToDb();

            if (NextChainLink != null)
            {
                NextChainLink.StartChain();
            }
        }
       
        // Method to map DTOs to the model collection
        public abstract List<T> MapToDb();
    }
}
