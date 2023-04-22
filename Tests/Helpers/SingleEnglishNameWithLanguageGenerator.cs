using ExternalApiHandler.DTOs;
using System.Linq;

namespace Tests.Helpers
{
    public class SingleEnglishNameWithLanguageGenerator
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
