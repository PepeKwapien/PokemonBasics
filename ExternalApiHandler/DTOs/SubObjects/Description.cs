namespace ExternalApiHandler.DTOs
{
    public class Description : ILanguageVersion
    {
        public string description { get; set; }
        public Name language { get; set; }
    }
}
