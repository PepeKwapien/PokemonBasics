using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Generations;
using Moq;
using System.Collections.Generic;
using Tests.TestHelpers;

namespace Tests.ExternalApiCrawler.Mappers
{
    [TestClass]
    public class GenerationMapperTests
    {
        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<GenerationDto> _generationDtos;
        private List<Generation> _generations;
        private GenerationMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new Mock<ILogger>();
            _databaseContext= new Mock<IPokemonDatabaseContext>();
            _generationDtos = new List<GenerationDto>()
            {
                new GenerationDto()
                {
                    name = "favorite",
                    names = new NameWithLanguage[]
                    {
                        new NameWithLanguage
                        {
                            name = "Favorite",
                            language = new Name
                            {
                                name = "en",
                            }
                        },
                        new NameWithLanguage
                        {
                            name = "Ulubiona",
                            language = new Name
                            {
                                name = "pl",
                            }
                        },
                    },
                    main_region = new Name
                    {
                        name = "hisui"
                    }
                }
            };

            _generations = new List<Generation>()
            {
                new Generation()
                {
                    Name = "Favorite",
                    Region = Regions.Hisui
                }
            };

            var gen = PokemonDbSetHelper.SetUpDbSetMock<Generation>(new List<Generation>());
            _databaseContext.Setup(dc => dc.Generations).Returns(gen.Object);

            _mapper = new GenerationMapper(_databaseContext.Object, _logger.Object);
            _mapper.SetUp(_generationDtos);
        }

        [TestMethod]
        public void MapsCorrectly_Simple()
        {
            // Arrange

            // Act
            var result = _mapper.MapToDb();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_generations.Count, result.Count);
            Assert.AreEqual(_generations[0].Name, result[0].Name);
            Assert.AreEqual(_generations[0].Region, result[0].Region);
        }
    }
}
