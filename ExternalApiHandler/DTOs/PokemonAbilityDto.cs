namespace ExternalApiHandler.DTOs
{
    internal class PokemonAbilityDto : IDto
    {
        public string name { get; set; }
        public bool is_main_series { get; set; }
        public EffectEntry[] effect_entries { get; set; }
    }
}
