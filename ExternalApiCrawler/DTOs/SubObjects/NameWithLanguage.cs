namespace ExternalApiCrawler.DTOs
{
    public class NameWithLanguage : Name, ILanguageVersion
    {
        public Name language { get; set; }
    }
}
