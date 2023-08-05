using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using Moq;
using PokemonAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using Tests.TestHelpers;

namespace Tests.PokemonAPI.Repositories
{
    [TestClass]
    public class PokemonTypeRepositoryTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private List<PokemonType> _types;
        private List<DamageMultiplier> _damageMultipliers;
        private IPokemonTypeRepository _typeRepository;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext= new Mock<IPokemonDatabaseContext>();

            PokemonType grass = new PokemonType()
            {
                Name = "Grass",
            };

            PokemonType water = new PokemonType()
            {
                Name = "Water"
            };

            PokemonType rock = new PokemonType()
            {
                Name = "Rock"
            };

            _types = new List<PokemonType>()
            {
                grass,
                water,
                rock,
            };

            _damageMultipliers = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    Type = grass,
                    Against = water,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = grass,
                    Against = rock,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = water,
                    Against = rock,
                    Multiplier = 2
                }
            };

            var types = PokemonDbSetHelper.SetUpDbSetMock<PokemonType>(_types);
            var damageMultiplier = PokemonDbSetHelper.SetUpDbSetMock<DamageMultiplier>(_damageMultipliers);
            _databaseContext.Setup(db => db.Types).Returns(types.Object);
            _databaseContext.Setup(db => db.DamageMultipliers).Returns(damageMultiplier.Object);

            _typeRepository = new PokemonTypeRepository(_databaseContext.Object);
        }

        [TestMethod]
        public void GetTypeCharacteristicByName_GetsEmptyListIfTypeNotExisting()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeCharacteristicByName("garbage");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTypeCharacteristicByName_GetsListWhenTypeIsAttacking()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeCharacteristicByName("grass");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(dm => dm.Type.Name.Equals("Grass")));
            Assert.IsTrue(result.Any(dm => dm.Against.Name.Equals("Water")));
            Assert.IsTrue(result.Any(dm => dm.Against.Name.Equals("Rock")));
        }

        [TestMethod]
        public void GetTypeCharacteristicByName_GetsListWhenTypeIsDefending()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeCharacteristicByName("rock");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(dm => dm.Against.Name.Equals("Rock")));
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Water")));
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Grass")));
        }

        [TestMethod]
        public void GetTypeCharacteristicByName_GetsListWhenTypeIsBothAttackingAndDefending()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeCharacteristicByName("water");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Water") && dm.Against.Name.Equals("Rock")));
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Grass") && dm.Against.Name.Equals("Water")));
        }

        [TestMethod]
        public void GetTypeDefensiveCharacteristicByName_GetsListWhenTypeIsDefending()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeDefensiveCharacteristicByName("rock");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(dm => dm.Against.Name.Equals("Rock")));
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Water")));
            Assert.IsTrue(result.Any(dm => dm.Type.Name.Equals("Grass")));
        }

        [TestMethod]
        public void GetTypeDefensiveCharacteristicByName_GetsEmptyListIfTypeHasNoWeakness()
        {
            // Arrange

            // Act
            var result = _typeRepository.GetTypeDefensiveCharacteristicByName("Grass");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
