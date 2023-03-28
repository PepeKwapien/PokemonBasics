namespace ExternalApiHandler.DTOs
{
    internal class PokemonMoveDto : IDto
    {
        public string name { get; set; }
        public int accuracy { get; set; }
        public int pp { get; set; }
        public int priority { get; set; }
        public int power { get; set; }
        public int? effect_chance { get; set; }
        public ContainsName damage_class { get; set; }
        public EffectEntry[] effect_entries { get; set; }
        public ContainsName type { get; set; }
    }
}
