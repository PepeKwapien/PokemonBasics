using ExternalApiCrawler.DTOs;

namespace ExternalApiCrawler.Helpers
{
    public class LanguageVersionHelper
    {
        public static T FindEnglishVersion<T>(T[] languageVersions) where T : ILanguageVersion
        {
            return FindLanguageVersion<T>(languageVersions, "en");
        }
        private static T FindLanguageVersion<T>(T[] languageVersions ,string language) where T : ILanguageVersion
        {
            return languageVersions.FirstOrDefault(name => name.language.name.Equals(language));
        }
    }
}
