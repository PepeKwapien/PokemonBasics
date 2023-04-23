using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Pokedexes;
using Models.Types;
using Moq;
using System.Collections.Generic;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class PokedexMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokedexDto> _pokedexDtos;
        private Pokedex _expectedPokedex;
        private PokedexMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext= new Mock<IPokemonDatabaseContext>();

            _pokedexDtos = new List<PokedexDto>()
            {
                new PokedexDto()
                {
                    name = "pepedex",
                    names = SingleEnglishNameGenerator.Generate("Pepedex"),
                    region = new Name
                    {
                        name = "sinnoh"
                    },
                    descriptions = new Description[]
                    {
                        new Description
                        {
                            description = "Pepe's dex B)",
                            language = new Name
                            {
                                name = "en"
                            }
                        }
                    }
                }
            };

            _expectedPokedex = new Pokedex
            {
                Name = "Pepedex",
                Region = Regions.Sinnoh,
                Description = "Pepe's dex B)",
            };

            var pokedexSet = PokemonDatabaseContextMock.SetUpDbSetMock<Pokedex>(new List<Pokedex>());
            _databaseContext.Setup(dc => dc.Pokedexes).Returns(pokedexSet.Object);

            _mapper = new PokedexMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_pokedexDtos);
        }

        [TestMethod]
        public void MapsCorrectly()
        {
            // Arrange

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokedexDtos.Count, result.Count);
            Assert.AreEqual(_expectedPokedex.Name, result[0].Name);
            Assert.AreEqual(_expectedPokedex.Region, result[0].Region);
            Assert.AreEqual(_expectedPokedex.Description, result[0].Description);
        }

        [TestMethod]
        public void MapsCorrectlyAndRegionIsEmpty()
        {
            // Arrange
            _pokedexDtos[0].region = null;

            // Act
            var result = _mapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokedexDtos.Count, result.Count);
            Assert.IsNull(result[0].Region);
        }
    }
}
