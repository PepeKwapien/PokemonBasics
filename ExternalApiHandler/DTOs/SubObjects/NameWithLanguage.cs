namespace ExternalApiHandler.DTOs
{
    public class NameWithLanguage : Name, ILanguageVersion
    {
        public Name language { get; set; }
    }
}
