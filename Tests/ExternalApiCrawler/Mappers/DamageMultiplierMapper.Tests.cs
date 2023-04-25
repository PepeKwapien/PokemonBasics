using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using ExternalApiCrawler.Mappers;
using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using Moq;
using System;
using System.Collections.Generic;
using Tests.Helpers;
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
                    names = SingleEnglishNameGenerator.Generate(_typeName1),
                    damage_relations = new TypeDamageRelations()
                    {
                        no_damage_from = new Name[0],
                        no_damage_to= new Name[0],
                        half_damage_from= new Name[0],
                        half_damage_to=new Name[0],
                        double_damage_from=new Name[0],
                        double_damage_to= new Name[0]
                    }
                },
                new PokemonTypeDto()
                {
                    name = _typeName2.ToLower(),
                    names = SingleEnglishNameGenerator.Generate(_typeName2),
                    damage_relations = new TypeDamageRelations()
                    {
                        no_damage_from = new Name[0],
                        no_damage_to= new Name[0],
                        half_damage_from= new Name[0],
                        half_damage_to=new Name[0],
                        double_damage_from=new Name[0],
                        double_damage_to= new Name[0]
                    }
                },
            };

            _pokemonTypes = new List<PokemonType>()
            {
                new PokemonType()
                {
                    Id = Guid.NewGuid(),
                    Name = _typeName1,
                    Color = TypeColorHelper.GetTypeColor(_typeName1),
                },
                new PokemonType()
                {
                    Id = Guid.NewGuid(),
                    Name = _typeName2,
                    Color = TypeColorHelper.GetTypeColor(_typeName2),
                },
            };


            var dm = PokemonDatabaseContextMock.SetUpDbSetMock<DamageMultiplier>(new List<DamageMultiplier>());
            _databaseContext.Setup(dc => dc.DamageMultipliers).Returns(dm.Object);
            var pt = PokemonDatabaseContextMock.SetUpDbSetMock<PokemonType>(_pokemonTypes);
            _databaseContext.Setup(dc => dc.Types).Returns(pt.Object);

            _damageMultiplierMapper = new DamageMultiplierMapper(_databaseContext.Object, _logger.Object);
        }

        [TestMethod]
        public void MapsCorrectly_Simple()
        {
            // Arrange
            _pokemonTypeDtos[0].damage_relations.double_damage_to = new Name[] { new Name() { name = _typeName2 }};
            _pokemonTypeDtos[1].damage_relations.half_damage_to = new Name[] { new Name() { name = _typeName2 }};
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>()
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
            }; ;

            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            AssertCollectionsAreEquivalent(damageMultipliers, result);
        }

        [TestMethod]
        public void MapsCorrectly_DuplicatesWithinOneRelationAreNotCreated()
        {
            // Arrange
            _pokemonTypeDtos[0].damage_relations.half_damage_from = new Name[] { new Name() { name = _typeName2 }, new Name() { name = _typeName2 } };
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[1].Id,
                    Type = _pokemonTypes[1],
                    AgainstId = _pokemonTypes[0].Id,
                    Against= _pokemonTypes[0],
                    Multiplier=0.5
                },
            }; ;

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            AssertCollectionsAreEquivalent(damageMultipliers, result);
        }

        [TestMethod]
        public void MapsCorrectly_DuplicatesWithinOneTypeAreNotCreated()
        {
            // Arrange
            _pokemonTypeDtos[0].damage_relations.half_damage_from = new Name[] { new Name() { name = _typeName1 }};
            _pokemonTypeDtos[0].damage_relations.half_damage_to = new Name[] { new Name() { name = _typeName1 }};
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[0].Id,
                    Type = _pokemonTypes[0],
                    AgainstId = _pokemonTypes[0].Id,
                    Against= _pokemonTypes[0],
                    Multiplier=0.5
                },
            }; ;

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            AssertCollectionsAreEquivalent(damageMultipliers, result);
        }

        [TestMethod]
        public void MapsCorrectly_DuplicatesAcrossAllTypesAreNotCreated()
        {
            // Arrange
            _pokemonTypeDtos[0].damage_relations.double_damage_to = new Name[] { new Name() { name = _typeName2 } };
            _pokemonTypeDtos[1].damage_relations.double_damage_from = new Name[] { new Name() { name = _typeName1 } };
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[0].Id,
                    Type = _pokemonTypes[0],
                    AgainstId= _pokemonTypes[1].Id,
                    Against= _pokemonTypes[1],
                    Multiplier=2
                },
            }; ;

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            AssertCollectionsAreEquivalent(damageMultipliers, result);
        }

        [TestMethod]
        public void MapsCorrectly_DuplicatesAreNotCreated()
        {
            // Arrange
            _pokemonTypeDtos[0].damage_relations.double_damage_to = new Name[] { new Name() { name = _typeName2 } };
            _pokemonTypeDtos[0].damage_relations.half_damage_to = new Name[] { new Name() { name = _typeName1 } };
            _pokemonTypeDtos[0].damage_relations.half_damage_from = new Name[] { new Name() { name = _typeName2 }, new Name() { name = _typeName2 }, new Name() { name = _typeName1 } };
            _pokemonTypeDtos[1].damage_relations.double_damage_from = new Name[] { new Name() { name = _typeName1 } };
            _damageMultiplierMapper.SetUp(_pokemonTypeDtos);
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[0].Id,
                    Type = _pokemonTypes[0],
                    AgainstId = _pokemonTypes[1].Id,
                    Against= _pokemonTypes[1],
                    Multiplier= 2
                },
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[0].Id,
                    Type = _pokemonTypes[0],
                    AgainstId = _pokemonTypes[0].Id,
                    Against= _pokemonTypes[0],
                    Multiplier= 0.5
                },
                new DamageMultiplier()
                {
                    TypeId = _pokemonTypes[1].Id,
                    Type = _pokemonTypes[1],
                    AgainstId = _pokemonTypes[0].Id,
                    Against= _pokemonTypes[0],
                    Multiplier= 0.5
                }, 
            }; ;

            // Act
            var result = _damageMultiplierMapper.Map();

            // Assert
            Assert.IsNotNull(result);
            AssertCollectionsAreEquivalent(damageMultipliers, result);
        }

        private void AssertCollectionsAreEquivalent(List<DamageMultiplier> actual, List<DamageMultiplier> expected)
        {
            Assert.AreEqual(actual.Count, expected.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(actual[i].Type.Name, expected[i].Type.Name);
                Assert.AreEqual(actual[i].Against.Name, expected[i].Against.Name);
                Assert.AreEqual(actual[i].Multiplier, expected[i].Multiplier);
            }
        }
    }
}
