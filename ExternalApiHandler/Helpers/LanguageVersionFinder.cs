using ExternalApiHandler.DTOs;

namespace ExternalApiHandler.Helpers
{
    public class LanguageVersionFinder
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
