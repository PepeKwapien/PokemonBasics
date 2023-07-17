namespace ExternalApiCrawler.DTOs
{
    public class PokedexDto : IDto, IMultiLanguageNames
    {
        public Description[] descriptions { get; set; }
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public PokemonEntryDto[] pokemon_entries { get; set; }
        public Name? region { get; set; }
        public Name[] version_groups { get; set; }
    }
}
