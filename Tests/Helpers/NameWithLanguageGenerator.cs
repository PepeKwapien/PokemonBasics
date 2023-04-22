using ExternalApiHandler.DTOs;
using System.Linq;

namespace Tests.Helpers
{
    public class NameWithLanguageGenerator
    {
        public static NameWithLanguage[] Generate(string[] names) {
            return names.Select(name => new NameWithLanguage()
            {
                name = name,
                language = new Name
                {
                    name = "en"
                }
            })
                .ToArray();
        }
    }
}
