using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using PokemonAPI.Helpers;

namespace Tests.PokemonAPI.Helpers
{
    [TestClass]
    public class DamageMultiplierHelperTests
    {
        [TestMethod]
        public void IsTypeAttacking_ReturnsTrueIfAttacking()
        {
            // Arrange
            PokemonType attacker = new()
            {
                Name = "Grass"
            };
            DamageMultiplier damageMultiplier = new()
            {
                Multiplier = 2,
                Type = attacker,
                Against = new()
                {
                    Name = "Water"
                }
            };

            // Act
            bool result = DamageMultiplierHelper.IsTypeAttacking(attacker, damageMultiplier);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTypeAttacking_ReturnsFalseIfDefending()
        {
            // Arrange
            PokemonType defender = new()
            {
                Name = "Grass"
            };
            DamageMultiplier damageMultiplier = new()
            {
                Multiplier = 2,
                Type = new()
                {
                    Name = "Fire"
                },
                Against = defender
            };

            // Act
            bool result = DamageMultiplierHelper.IsTypeAttacking(defender, damageMultiplier);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
