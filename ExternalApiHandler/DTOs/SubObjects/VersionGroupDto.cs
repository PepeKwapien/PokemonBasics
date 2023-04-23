namespace ExternalApiCrawler.DTOs
{
    public class VersionGroupDto
    {
        public Name generation { get; set; }
        public string name { get; set; }
        public NameWithUrl[] versions { get; set; }
        public Name[] pokedexes { get; set; }
        public Name[] regions { get; set; }
    }
}
