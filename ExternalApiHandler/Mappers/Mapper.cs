using DataAccess;
using Models;

namespace ExternalApiCrawler.Mappers
{
    public abstract class Mapper
    {
        protected readonly IPokemonDatabaseContext _dbContext;

        protected Mapper(IPokemonDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Mapper NextHandler { get; set; }

        // Method to create a chain in a fluent API style
        public Mapper Next(Mapper nextHandler)
        {
            NextHandler = nextHandler;

            return nextHandler;
        }
        public void StartChain()
        {
            MapAndSave();

            if (NextHandler != null)
            {
                NextHandler.StartChain();
            }
        }
       
        // Method to map DTOs to the model collection
        public abstract void MapAndSave();
    }
}
