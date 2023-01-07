using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Attacks;
using Models.Types;
using Tests.Helpers;

namespace Tests.Models.Attacks
{
    [TestClass]
    public class AttackTests
    {
        private PropertyHelper<Attack> _propertyHelper;

        public AttackTests()
        {
            _propertyHelper = new PropertyHelper<Attack>();

        }

        [TestMethod]
        public void HasId()
        {
            // Arrange
            string propertyName = "Id";

            // Act
            var idProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(idProperty);
            Assert.AreEqual(idProperty.PropertyType.Name, nameof(System.Guid));
        }

        [TestMethod]
        public void HasIdCaseSensitive()
        {
            // Arrange
            string propertyName = "id";

            // Act
            var idPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var idPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(idPropertyCaseInsensitive);
            Assert.AreEqual(idPropertyCaseInsensitive.PropertyType.Name, nameof(System.Guid));

            Assert.IsTrue(idPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasName()
        {
            // Arrange
            string propertyName = "Name";

            // Act
            var nameProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(nameProperty);
            Assert.AreEqual(nameProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasNameCaseSensitive()
        {
            // Arrange
            string propertyName = "name";

            // Act
            var namePropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var namePropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(namePropertyCaseInsensitive);
            Assert.AreEqual(namePropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(namePropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasPower()
        {
            // Arrange
            string propertyName = "Power";

            // Act
            var powerProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(powerProperty);
            Assert.AreEqual(powerProperty.PropertyType.Name, nameof(System.Int32));
        }

        [TestMethod]
        public void HasPowerCaseSensitive()
        {
            // Arrange
            string propertyName = "power";

            // Act
            var powerPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var powerPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(powerPropertyCaseInsensitive);
            Assert.AreEqual(powerPropertyCaseInsensitive.PropertyType.Name, nameof(System.Int32));

            Assert.IsTrue(powerPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasAccuracy()
        {
            // Arrange
            string propertyName = "Accuracy";

            // Act
            var accuracyProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(accuracyProperty);
            Assert.AreEqual(accuracyProperty.PropertyType.Name, nameof(System.Int32));
        }

        [TestMethod]
        public void HasAccuracyCaseSensitive()
        {
            // Arrange
            string propertyName = "accuracy";

            // Act
            var accuracyPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var accuracyPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(accuracyPropertyCaseInsensitive);
            Assert.AreEqual(accuracyPropertyCaseInsensitive.PropertyType.Name, nameof(System.Int32));

            Assert.IsTrue(accuracyPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasPP()
        {
            // Arrange
            string propertyName = "PP";

            // Act
            var ppProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(ppProperty);
            Assert.AreEqual(ppProperty.PropertyType.Name, nameof(System.Int32));
        }

        [TestMethod]
        public void HasPPCaseSensitive()
        {
            // Arrange
            string propertyName = "pp";

            // Act
            var ppPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var ppPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(ppPropertyCaseInsensitive);
            Assert.AreEqual(ppPropertyCaseInsensitive.PropertyType.Name, nameof(System.Int32));

            Assert.IsTrue(ppPropertyCaseSensitive == null);
        }

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
        public void HasCategory()
        {
            // Arrange
            string propertyName = "Category";

            // Act
            var categoryProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(categoryProperty);
            Assert.AreEqual(categoryProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasCategoryCaseSensitive()
        {
            // Arrange
            string propertyName = "category";

            // Act
            var categoryPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var categoryPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(categoryPropertyCaseInsensitive);
            Assert.AreEqual(categoryPropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(categoryPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasSpecialEffect()
        {
            // Arrange
            string propertyName = "SpecialEffect";

            // Act
            var specialEffectProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(specialEffectProperty);
            Assert.AreEqual(specialEffectProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasSpecialEffectCaseSensitive()
        {
            // Arrange
            string propertyName = "specialeffect";

            // Act
            var specialEffectPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var specialEffectPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(specialEffectPropertyCaseInsensitive);
            Assert.AreEqual(specialEffectPropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(specialEffectPropertyCaseSensitive == null);
        }
    }
}
