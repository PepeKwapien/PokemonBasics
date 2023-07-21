using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonAPI.Helpers;

namespace Tests.PokemonAPI.Helpers
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void Levenshtein_Zero()
        {
            // Arrange
            string string1 = "bulbasaur";
            string string2 = "bulbasaur";

            // Act
            int result = StringHelper.LevenshteinDistance(string1, string2);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Levenshtein_One()
        {
            // Arrange
            string string1 = "monkey";
            string string2 = "mankey";

            // Act
            int result = StringHelper.LevenshteinDistance(string1, string2);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Levenshtein_Eight()
            // Eight (squirtle's length) char replacements
        {
            // Arrange
            string string1 = "squirtle";
            string string2 = "pikachu";

            // Act
            int result = StringHelper.LevenshteinDistance(string1, string2);

            // Assert
            Assert.AreEqual(8, result);
        }
    }
}
