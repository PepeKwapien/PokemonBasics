namespace ExternalApiHandler.DTOs
{
    public class InnerPokemonMove
    {
        public Name move { get; set; }
        public VersionGroupDetails[] version_group_details { get; set; }
    }
}
