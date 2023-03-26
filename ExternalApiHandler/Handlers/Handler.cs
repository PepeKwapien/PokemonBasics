using ExternalApiHandler.DTOs;

namespace ExternalApiHandler.Handlers
{
    internal abstract class Handler<T> where T : IDto
    {
        protected Handler(Handler<IDto> nextHandler, List<T> collection)
        {
            NextHandler = nextHandler;
            Collection = collection;
        }
        protected Handler(Handler<IDto> nextHandler)
        {
           NextHandler= nextHandler;
        }
        protected Handler() { }

        public Handler<IDto> NextHandler { get; set; }
        public List<T> Collection { get; set; }

        // Method to create a chain in a fluent API style
        public Handler<IDto> Next(Handler<IDto> nextHandler)
        {
            NextHandler= nextHandler;

            return nextHandler;
        }
        public void StartChain()
        {
            Handle();

            if (NextHandler != null)
            {
                NextHandler.StartChain();
            }
        }
        public void Handle(List<T> values)
        {
            Collection = values;
            Handle();
        }
        // Method has to be implemented in derrived class and it has to work on a Collection property
        public abstract void Handle();
    }
}
