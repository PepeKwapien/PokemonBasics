using DataAccess;
using Models;

namespace ExternalApiHandler.Mappers
{
    public abstract class Mapper<T> where T : IModel
    {
        protected readonly IPokemonDatabaseContext _dbContext;

        protected Mapper(IPokemonDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Mapper<IModel> NextHandler { get; set; }

        // Method to create a chain in a fluent API style
        public Mapper<IModel> Next(Mapper<IModel> nextHandler)
        {
            NextHandler= nextHandler;

            return nextHandler;
        }
        public void StartChain()
        {
            Map();

            if (NextHandler != null)
            {
                NextHandler.StartChain();
            }
        }
       
        // Method to map DTOs to the model collection
        public abstract List<T> Map();
    }
}
