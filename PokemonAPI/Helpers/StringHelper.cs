using Microsoft.Extensions.Hosting;

namespace PokemonAPI.Helpers
{
    public class StringHelper
    {
        public static int LevenshteinDistance(string firstLiteral, string secondLiteral)
        {
            int firstLiteralLength = firstLiteral.Length;
            int secondLiteralLength = secondLiteral.Length;
            int[,] dimension = new int[firstLiteralLength + 1, secondLiteralLength + 1];

            for(int i = 0; i <= firstLiteralLength; i++)
            {
                dimension[i, 0] = i;
            }
            for(int i = 1; i <= secondLiteralLength; i++)
            {
                dimension[0, i] = i;
            }

            for (int i = 1; i <= firstLiteralLength; i++)
            {
                for (int j = 1; j <= secondLiteralLength; j++)
                {
                    int cost = firstLiteral[i - 1] != secondLiteral[j - 1] ? 1 : 0;
                    dimension[i, j] = new[] { dimension[i - 1, j] + 1, dimension[i, j - 1] + 1, dimension[i - 1, j - 1] + cost }.Min();
                }
            }

            return dimension[firstLiteralLength, secondLiteralLength];
        }
    }
}
