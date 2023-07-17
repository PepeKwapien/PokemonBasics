namespace ExternalApiCrawler.DTOs
{
    public class MoveDto : IDto, IMultiLanguageNames
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public int? accuracy { get; set; }
        public int? pp { get; set; }
        public int priority { get; set; }
        public int? power { get; set; }
        public int? effect_chance { get; set; }
        public Name damage_class { get; set; }
        public Name generation { get; set; }
        public EffectEntry[] effect_entries { get; set; }
        public Name type { get; set; }
        public Name target { get; set; }
    }
}
