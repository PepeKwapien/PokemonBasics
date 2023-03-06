namespace ExternalApiHandler.Requesters
{
    internal abstract class Requester
    {
        private readonly Requester _nextHandler;

        protected Requester(Requester nextHandler)
        {
            _nextHandler = nextHandler;
        }

        protected Requester() { }

        public async Task Start()
        {
            await Action();

            if (_nextHandler!= null)
            {
                await _nextHandler.Start();
            }
        }

        public virtual async Task Action()
        {

        }
    }
}
