namespace ExternalApiHandler.DTOs
{
    internal class PokedexDto : IDto
    {
        public Description[] descriptions { get; set; }
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public PokemonEntry[] pokemon_entries { get; set; }
        public string? region { get; set; }
        public Name[] version_groups { get; set; }
    }
}
