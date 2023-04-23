namespace ExternalApiCrawler.DTOs
{
    public class EffectEntry : ILanguageVersion
    {
        public string effect { get; set; }
        public Name language { get; set; }
    }
}
