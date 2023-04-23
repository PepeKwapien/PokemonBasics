using ExternalApiCrawler.DTOs;
using System.Globalization;

namespace ExternalApiCrawler.Helpers
{
    public class StringHelper
    {
        public static string Normalize(string text)
        {
            string[] textParts = text.Split(new char[] {'_', '-', ' '});
            string[] titleCaseParts = textParts.Select(part => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(part)).ToArray();
            return String.Join(" ", titleCaseParts);
        }

        public static string NormalizeAndJoinNames(Name[] names)
        {
            List<string> result = new List<string>();

            foreach (Name name in names)
            {
                string[] textParts = name.name.Split(new char[] { '_', '-', ' ' });
                string[] titleCaseParts = textParts.Select(part => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(part)).ToArray();
                result.Add(String.Join(" ", titleCaseParts));
            }

            return String.Join(", ", result);
        }
    }
}
