namespace ExternalApiCrawler.DTOs
{
    public class PokemonSpeciesDto : IDto, IMultiLanguageNames
    {
        public Name[] egg_groups { get; set; }
        public Name generation { get; set; }
        public Genera[] genera { get; set; }
        public Name habitat { get; set; }
        public bool has_gender_differences { get; set; }
        public bool is_baby { get; set; }
        public bool is_legendary { get; set; }
        public bool is_mythical { get; set; }
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public Name shape { get; set; }
        public Variety[] varieties { get; set; }
    }
}
