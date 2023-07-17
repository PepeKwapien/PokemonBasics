namespace ExternalApiCrawler.DTOs
{
    public interface IMultiLanguageNames
    {
        string name { get; set; }
        NameWithLanguage[] names { get; set; }
    }
}
