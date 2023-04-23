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
    }
}
