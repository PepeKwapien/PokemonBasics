using ExternalApiCrawler.DTOs;

namespace Tests.TestHelpers
{
    public class SingleEnglishNameHelper
    {
        public static NameWithLanguage[] Generate(string name) {
            return new NameWithLanguage[]
            {
                new NameWithLanguage()
                {
                    name = name,
                    language = new Name
                    {
                        name = "en"
                    }
                }
            };
        }
    }
}
