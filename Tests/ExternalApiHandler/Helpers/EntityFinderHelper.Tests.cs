using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Generations;
using Models.Types;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tests.Helpers;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Helpers
{
    [TestClass]
    public class EntityFinderHelperTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext= new Mock<IPokemonDatabaseContext>();
        }

        [TestMethod]
        public void FindTypeByNameCaseInsensitive_FindsCorrectTypeWhenNameMatching()
        {
            // Arrange
            string[] typeNames = new string[] { "Ice, Grass, Water, Fire, Electric, Ground" };
            List<PokemonType> types = typeNames.ToList().Select(typeName => new PokemonType { Name = typeName }).ToList();

            var typeSet = PokemonDatabaseContextMock.SetUpDbSetMock(types);
            _databaseContext.Setup(dbc => dbc.Types).Returns(typeSet.Object);

            int randomIndex = new Random().Next(typeNames.Length);

            // Act
            var foundType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_databaseContext.Object, typeNames[randomIndex]);

            // Assert
            Assert.IsNotNull(foundType);
            Assert.AreEqual(types[randomIndex].Name, foundType.Name);
        }

        [TestMethod]
        public void FindTypeByNameCaseInsensitive_FindsCorrectTypeWithLowercaseName()
        {
            // Arrange
            string[] typeNames = new string[] { "ice, grass, water, fire, electric, ground" };
            List<PokemonType> types = typeNames.ToList().Select(typeName => new PokemonType
            {
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(typeName)
            })
                .ToList();

            var typeSet = PokemonDatabaseContextMock.SetUpDbSetMock(types);
            _databaseContext.Setup(dbc => dbc.Types).Returns(typeSet.Object);

            int randomIndex = new Random().Next(typeNames.Length);

            // Act
            var foundType = EntityFinderHelper.FindTypeByNameCaseInsensitive(_databaseContext.Object, typeNames[randomIndex]);

            // Assert
            Assert.IsNotNull(foundType);
            Assert.AreEqual(types[randomIndex].Name, foundType.Name);
        }

        [TestMethod]
        public void FindGenerationByDtoName_FindsCorrectGeneration()
        {
            // Arrange
            string nameToSearch = "second";
            string englishNameToFind = "2";

            List<GenerationDto> generationDtos = new List<GenerationDto>()
            {
                new GenerationDto
                {
                    name = "first",
                    names = EnglishNameArrayGenerator.Generate(new []{"1"})
                },
                new GenerationDto
                {
                    name = nameToSearch,
                    names = EnglishNameArrayGenerator.Generate(new []{englishNameToFind})
                },
                new GenerationDto
                {
                    name = "third",
                    names = EnglishNameArrayGenerator.Generate(new []{"3"})
                },
            };

            List<Generation> generations = new List<Generation>()
            {
                new Generation
                {
                    Name = "1",
                    Region = Regions.Kanto
                },
                new Generation
                {
                    Name = "2",
                    Region = Regions.Johto
                },
                new Generation
                {
                    Name = "3",
                    Region = Regions.Hoenn
                }
            };

            var generationSet = PokemonDatabaseContextMock.SetUpDbSetMock(generations);
            _databaseContext.Setup(dbc => dbc.Generations).Returns(generationSet.Object);

            // Act
            var result = EntityFinderHelper.FindGenerationByDtoName(_databaseContext.Object, nameToSearch, generationDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(englishNameToFind, result.Name);
        }
    }
}
