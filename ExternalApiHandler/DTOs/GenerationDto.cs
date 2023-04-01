namespace ExternalApiHandler.DTOs
{
    internal class GenerationDto : IDto
    {
        public Name[] abilities { get; set; }
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public Name main_region { get; set; }
        public Name[] moves { get; set; }
        public Name[] pokemon_species { get; set; }
        public Name[] version_groups { get; set; }
    }
}
