namespace ExternalApiHandler.DTOs
{
    public interface IMultiLanguageNames
    {
        string name { get; set; }
        NameWithLanguage[] names { get; set; }
    }
}
