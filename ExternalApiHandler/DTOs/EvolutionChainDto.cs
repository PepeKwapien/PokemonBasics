namespace ExternalApiHandler.DTOs
{
    internal class EvolutionChainDto : IDto
    {
        public string? baby_trigger_item { get; set; }
        public EvolvesTo chain { get; set; }
    }
}
