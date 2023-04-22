namespace ExternalApiHandler.DTOs
{
    public class PokemonDto : IDto
    {
        public PokemonAbilityDto[] abilities { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public bool is_default { get; set; }
        public string name { get; set; }
        public InnerPokemonType[] types { get; set; }
        public Stat[] stats { get; set; }
        public Name species { get; set; }
        public InnerPokemonMove[] moves { get; set; }
    }
}
