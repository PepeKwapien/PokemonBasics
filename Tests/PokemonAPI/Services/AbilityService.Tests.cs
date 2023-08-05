using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Moq;
using PokemonAPI.Repositories;
using PokemonAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace Tests.PokemonAPI.Services
{
    [TestClass]
    public class AbilityServiceTests
    {
        private Mock<IAbilityRepository> _abilityRepository;
        private IAbilityService _abilityService;

        [TestInitialize]
        public void Initialize()
        {
            _abilityRepository= new Mock<IAbilityRepository>();
            _abilityService = new AbilityService( _abilityRepository.Object);
        }

        [TestMethod]
        public void GetAbilitiesDtoForPokemon_MapsCorrectly()
        {
            // Arrange
            Ability firstAbility= new Ability()
            {
                Name = "stinky",
                Effect = "stinks"
            };
            Ability secondAbility = new Ability()
            {
                Name = "stunning",
                Effect = "stuns"
            };
            Ability thirdAbility = new Ability()
            {
                Name = "broken",
                Effect = "breaks"
            };

            List<PokemonAbility> pokemonAbilities= new List<PokemonAbility>()
            {
                new PokemonAbility
                {
                    Ability = firstAbility
                },
                new PokemonAbility
                {
                    Ability = secondAbility
                },
                new PokemonAbility
                {
                    Ability = thirdAbility
                }
            };

            _abilityRepository.Setup(ar => ar.GetAbilitiesForPokemon(It.IsAny<string>())).Returns(pokemonAbilities);

            // Act
            var result = _abilityService.GetAbilitiesDtoForPokemon("w/e");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            foreach(var ability in pokemonAbilities)
            {
                Assert.IsTrue(result.Any(abilityDto => abilityDto.Name.Equals(ability.Ability.Name) && abilityDto.Effect.Equals(ability.Ability.Effect)));
            }
        }
    }
}
