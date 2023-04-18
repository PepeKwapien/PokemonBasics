using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using ExternalApiHandler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using Moq;
using System.Collections.Generic;
using Tests.Mocks;

namespace Tests.ExternalApiHandler.Mappers
{
    [TestClass]
    public class DamageMultiplierMapperTests
    {
        private string _typeName1;
        private string _typeName2;

        private Mock<ILogger> _logger;
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonTypeDto> _pokemonTypeDtos;
        private List<PokemonType> _pokemonTypes;
        private List<DamageMultiplier> _damageMultipliers;
        private DamageMultiplierMapper _damageMultiplierMapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _typeName1 = "Fighting";
            _typeName2 = "Ice";
            _logger = new Mock<ILogger>();
            _databaseContext = new Mock<IPokemonDatabaseContext>();
            _pokemonTypeDtos = new List<PokemonTypeDto>()
            {
                new PokemonTypeDto()
                {
                    name = _typeName1.ToLower(),
                    names = new NameWithLanguage[]
                    {
                        new NameWithLanguage()
                        {
                            name = _typeName1,
                            language = new Name() { name = "en" }
                        }
                    },
                    damage_relations = new TypeDamageRelations()
                    {
                        no_damage_from = new Name[0],
                        no_damage_to= new Name[0],
                        half_damage_from= new Name[0],
                        half_damage_to=new Name[0],
                        double_damage_from=new Name[0],
                        double_damage_to= new Name[]{ new Name() { name = _typeName2} }
                    }
                },
                new PokemonTypeDto()
                {
                    name = _typeName2.ToLower(),
                    names = new NameWithLanguage[]
                    {
                        new NameWithLanguage()
                        {
                            name = _typeName2,
                            language = new Name() { name = "en" }
                        }
                    },
                    damage_relations = new TypeDamageRelations()
                    {
                        no_damage_from = new Name[0],
                        no_damage_to= new Name[0],
                        half_damage_from= new Name[0],
                        half_damage_to=new Name[]{ new Name() { name = _typeName2} },
                        double_damage_from=new Name[0],
                        double_damage_to= new Name[0]
                    }
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

            _damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    Type = _pokemonTypes[0],
                    Against= _pokemonTypes[1],
                    Multiplier=2
                },
                new DamageMultiplier()
                {
                    Type = _pokemonTypes[1],
                    Against= _pokemonTypes[1],
                    Multiplier=0.5
                },
            };

            var dm = PokemonDatabaseContextMock.SetUpDbSetMock<DamageMultiplier>(new List<DamageMultiplier>());
            _databaseContext.Setup(dc => dc.DamageMultipliers).Returns(dm.Object);
            var pt = PokemonDatabaseContextMock.SetUpDbSetMock<PokemonType>(_pokemonTypes);
            _databaseContext.Setup(dc => dc.Types).Returns(pt.Object);

            _damageMultiplierMapper = new DamageMultiplierMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void MapsCorrectly()
        {
            // Arrange
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_damageMultipliers.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(_damageMultipliers[i].Type.Name, result[i].Type.Name);
                Assert.AreEqual(_damageMultipliers[i].Against.Name, result[i].Against.Name);
                Assert.AreEqual(_damageMultipliers[i].Multiplier, result[i].Multiplier);
            }
        }

        [TestMethod]
        public void MapsCorrectlyButDuplicatesAreNotCreated()
        {
            // Arrange
            _pokemonTypeDtos[1].damage_relations.half_damage_from = new Name[] { new Name() { name = _typeName2 }, new Name() { name = _typeName1 } };
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_damageMultipliers.Count, result.Count);
        }
    }
}
