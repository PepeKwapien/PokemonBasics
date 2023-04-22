using DataAccess;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ExternalApiHandler.Mappers;
using System.Collections.Generic;
using ExternalApiHandler.DTOs;
using Models.Types;
using ExternalApiHandler.Helpers;
using Tests.Mocks;
using Tests.Helpers;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class PokemonTypeMapperTests
    {
        private string _typeName1;
        private string _typeName2;

        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonTypeDto> _pokemonTypeDtos;
        private List<PokemonType> _pokemonTypes;
        private PokemonTypeMapper _pokemonTypeMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _typeName1 = "Fighting";
            _typeName2 = "Ice";
            _logger = new Mock<ILogger>();
            _databaseContext= new Mock<IPokemonDatabaseContext>();
            _pokemonTypeDtos = new List<PokemonTypeDto>()
            {
                new PokemonTypeDto()
                {
                    name = _typeName1.ToLower(),
                    names = EnglishNameArrayGenerator.Generate(new []{_typeName1}),
                },
                new PokemonTypeDto()
                {
                    name = _typeName2.ToLower(),
                    names = EnglishNameArrayGenerator.Generate(new []{_typeName2}),
                },
            };
            _pokemonTypes = new List<PokemonType>()
            {
                new PokemonType()
                {
                    Name = _typeName1,
                    Color = TypeColorHelper.GetTypeColor(_typeName1),
                },
                new PokemonType()
                {
                    Name = _typeName2,
                    Color = TypeColorHelper.GetTypeColor(_typeName2),
                },
            };

            var dm = PokemonDatabaseContextMock.SetUpDbSetMock<DamageMultiplier>(new List<DamageMultiplier>());
            _databaseContext.Setup(dc => dc.DamageMultipliers).Returns(dm.Object);
            var pt = PokemonDatabaseContextMock.SetUpDbSetMock<PokemonType>(new List<PokemonType>());
            _databaseContext.Setup(dc => dc.Types).Returns(pt.Object);

            _pokemonTypeMapper = new PokemonTypeMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void MapsCorrectly()
        {
            // Arrange
            _pokemonTypeMapper.SetUp(_pokemonTypeDtos);

            // Act
            var result = _pokemonTypeMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_pokemonTypeDtos.Count, result.Count);
            for(int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_pokemonTypes[i].Name, result[i].Name);
                Assert.AreEqual(_pokemonTypes[i].Color, result[i].Color);
            }
        }
    }
}
