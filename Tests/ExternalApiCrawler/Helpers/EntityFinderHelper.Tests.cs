using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Enums;
using Models.Generations;
using Models.Pokemons;
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
        public void FindEntityByDtoName_FindsCorrectGeneration()
        {
            // Arrange
            string nameToSearch = "second";
            string englishNameToFind = "2";

            List<GenerationDto> generationDtos = new List<GenerationDto>()
            {
                new GenerationDto
                {
                    name = "first",
                    names = SingleEnglishNameGenerator.Generate("1")
                },
                new GenerationDto
                {
                    name = nameToSearch,
                    names = SingleEnglishNameGenerator.Generate(englishNameToFind)
                },
                new GenerationDto
                {
                    name = "third",
                    names = SingleEnglishNameGenerator.Generate("3")
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
            var result = EntityFinderHelper.FindEntityByDtoName(_databaseContext.Object.Generations, nameToSearch, generationDtos);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(englishNameToFind, result.Name);
        }

        [TestMethod]
        public void FindPokemonByName_FindsCorrectPokemon()
        {
            // Arrange
            List<Pokemon> pokemons = new List<Pokemon>()
            {
                new Pokemon
                {
                    Name = "Dani Devito"
                },
                new Pokemon
                {
                    Name = "Amy Kay"
                }
            };

            var pokemonSet = PokemonDatabaseContextMock.SetUpDbSetMock(pokemons);
            _databaseContext.Setup(dbc => dbc.Pokemons).Returns(pokemonSet.Object);

            // Act
            var result1 = EntityFinderHelper.FindPokemonByName(_databaseContext.Object.Pokemons, "dani-devito");
            var result2 = EntityFinderHelper.FindPokemonByName(_databaseContext.Object.Pokemons, "amy_kay");

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual(pokemons[0], result1);
            Assert.AreEqual(pokemons[1], result2);
        }
    }
}
