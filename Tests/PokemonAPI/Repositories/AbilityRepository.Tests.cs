using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Models.Pokemons;
using Moq;
using PokemonAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using Tests.TestHelpers;

namespace Tests.PokemonAPI.Repositories
{
    [TestClass]
    public class AbilityRepositoryTests
    {
        private Mock<IPokemonDatabaseContext> _dbContext;
        private List<PokemonAbility> _pokemonAbilities;
        private IAbilityRepository _abilityRepository;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = new Mock<IPokemonDatabaseContext>();
            _abilityRepository = new AbilityRepository(_dbContext.Object);
        }

        [TestMethod]
        public void GetAbilitiesForPokemon_GetsCorrectAbilitiesForPokemon()
        {
            // Arrange
            string desiredName = "Bulbasaur";
            string undesiredName = "Charmander";
            _pokemonAbilities = new List<PokemonAbility>
            {
                
                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = desiredName
                    }
                },
                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = desiredName
                    }
                },
                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = undesiredName
                    }
                },
            };

            var pokemonAbilitiesSet = PokemonDbSetHelper.SetUpDbSetMock(_pokemonAbilities);
            _dbContext.Setup(db => db.PokemonAbilities).Returns(pokemonAbilitiesSet.Object);

            // Act
            var result = _abilityRepository.GetAbilitiesForPokemon(desiredName.ToLower());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(pa => pa.Pokemon.Name == desiredName));
            Assert.IsFalse(result.Any(pa => pa.Pokemon.Name == undesiredName));
        }

        [TestMethod]
        public void GetAbilitiesForPokemon_GetsEmptyIfNoNamesMatch()
        {
            // Arrange
            _pokemonAbilities = new List<PokemonAbility>
            {

                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = "Bulbasaur"
                    }
                },
                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = "Bulbasaur"
                    }
                },
                new PokemonAbility()
                {
                    Pokemon = new Pokemon
                    {
                        Name = "Charmander"
                    }
                },
            };

            var pokemonAbilitiesSet = PokemonDbSetHelper.SetUpDbSetMock(_pokemonAbilities);
            _dbContext.Setup(db => db.PokemonAbilities).Returns(pokemonAbilitiesSet.Object);

            // Act
            var result = _abilityRepository.GetAbilitiesForPokemon("squirtle");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
