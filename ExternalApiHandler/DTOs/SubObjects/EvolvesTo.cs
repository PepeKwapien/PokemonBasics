namespace ExternalApiHandler.DTOs
{
    public class EvolvesTo
    {
        public EvolutionDetail[] evolution_details { get; set; }
        public EvolvesTo[] evolves_to { get; set; }
        public Name species { get; set; }
        public bool is_baby { get; set; }
    }
}
