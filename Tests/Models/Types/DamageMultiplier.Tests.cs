using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using Tests.Helpers;

namespace Tests.Models
{
    [TestClass]
    public class DamageMultiplierTests
    {
        private PropertyHelper<DamageMultiplier> _propertyHelper;

        public DamageMultiplierTests()
        {
            _propertyHelper = new PropertyHelper<DamageMultiplier>();
        }

        #region Properties
        [TestMethod]
        public void HasType()
        {
            // Arrange
            string propertyName = "Type";

            // Act
            var typeProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(typeProperty);
            Assert.AreEqual(typeProperty.PropertyType.Name, nameof(PokemonType));
        }

        [TestMethod]
        public void HasTypeCaseSensitive()
        {
            // Arrange
            string propertyName = "type";

            // Act
            var typePropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var typePropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(typePropertyCaseInsensitive);
            Assert.AreEqual(typePropertyCaseInsensitive.PropertyType.Name, nameof(PokemonType));

            Assert.IsTrue(typePropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasAgainst()
        {
            // Arrange
            string propertyName = "Against";

            // Act
            var AgainstProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(AgainstProperty);
            Assert.AreEqual(AgainstProperty.PropertyType.Name, nameof(PokemonType));
        }

        [TestMethod]
        public void HasAgainstCaseSensitive()
        {
            // Arrange
            string propertyName = "against";

            // Act
            var AgainstPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var AgainstPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(AgainstPropertyCaseInsensitive);
            Assert.AreEqual(AgainstPropertyCaseInsensitive.PropertyType.Name, nameof(PokemonType));

            Assert.IsTrue(AgainstPropertyCaseSensitive == null);
        }


        [TestMethod]
        public void HasMultiplier()
        {
            // Arrange
            string propertyName = "Multiplier";

            // Act
            var multiplierProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(multiplierProperty);
            Assert.AreEqual(multiplierProperty.PropertyType.Name, nameof(System.Double));
        }

        [TestMethod]
        public void HasMultiplierCaseSensitive()
        {
            // Arrange
            string propertyName = "multiplier";

            // Act
            var multiplierPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var multiplierPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(multiplierPropertyCaseInsensitive);
            Assert.AreEqual(multiplierPropertyCaseInsensitive.PropertyType.Name, nameof(System.Double));

            Assert.IsTrue(multiplierPropertyCaseSensitive == null);
        }

        #endregion
    }
}
