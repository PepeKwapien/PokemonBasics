namespace ExternalApiHandler.DTOs
{
    internal class PokeballDto : IDto
    {
        public string name { get; set; }
        public NameWithLanguage names { get; set; }
        public EffectEntry[] effect_entries { get; set; }
        public GameIndice[] game_indices { get; set; }
    }
}
