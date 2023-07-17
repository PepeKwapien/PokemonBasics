using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ExternalApiCrawler.Helpers
{
    [TestClass]
    public class LanguageVersionHelperTests
    {
        private NameWithLanguage[] _languagesVersions;
        private NameWithLanguage _englishVersion;
        private NameWithLanguage _germanVersion;
        private NameWithLanguage _frenchVersion;
        private NameWithLanguage _japaneseVersion;

        [TestInitialize]
        public void Initialize()
        {
            _englishVersion = new NameWithLanguage()
            {
                name = "Bulbasaur",
                language = new Name()
                {
                    name = "en"
                }
            };
            _germanVersion = new NameWithLanguage()
            {
                name = "Bisasam",
                language = new Name()
                {
                    name = "de"
                }
            };
            _frenchVersion = new NameWithLanguage()
            {
                name = "Bulbizarre",
                language = new Name()
                {
                    name = "fr"
                }
            };
            _japaneseVersion = new NameWithLanguage()
            {
                name = "フシギダネ",
                language = new Name()
                {
                    name = "jp"
                }
            };
        }

        [TestMethod]
        public void FindsEnglishCorrectlyWhenThereIsOne()
        {
            // Arrange
            _languagesVersions = new NameWithLanguage[]
            {
               _japaneseVersion, _germanVersion, _englishVersion, _frenchVersion
            };

            // Act
            var result = LanguageVersionHelper.FindEnglishVersion(_languagesVersions);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("en", result.language.name);
            Assert.AreEqual(_englishVersion.name, result.name);
        }

        [TestMethod]
        public void FindsEnglishCorrectlyFirstWhenThereAreMore()
        {
            // Arrange
            _languagesVersions = new NameWithLanguage[]
            {
               _japaneseVersion, _germanVersion, _englishVersion, _frenchVersion,
               new NameWithLanguage()
                {
                    name = "Impostor",
                    language = new Name()
                    {
                        name = "en"
                    }
                },
               new NameWithLanguage()
                {
                    name = "Another impostor",
                    language = new Name()
                    {
                        name = "en"
                    }
                }
            };

            // Act
            var result = LanguageVersionHelper.FindEnglishVersion(_languagesVersions);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("en", result.language.name);
            Assert.AreEqual(_englishVersion.name, result.name);
        }
    }
}
