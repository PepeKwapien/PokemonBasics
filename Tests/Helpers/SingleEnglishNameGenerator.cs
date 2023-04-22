using ExternalApiHandler.DTOs;

namespace Tests.Helpers
{
    public class SingleEnglishNameGenerator
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
