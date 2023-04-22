namespace ExternalApiHandler.DTOs
{
    public class PokeballDto : IDto, IMultiLanguageNames
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public EffectEntry[] effect_entries { get; set; }
        public GameIndice[] game_indices { get; set; }
    }
}
