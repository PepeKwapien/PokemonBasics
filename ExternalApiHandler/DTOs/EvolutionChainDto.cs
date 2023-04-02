namespace ExternalApiHandler.DTOs
{
    internal class EvolutionChainDto : IDto
    {
        public Name? baby_trigger_item { get; set; }
        public EvolvesTo chain { get; set; }
    }
}
