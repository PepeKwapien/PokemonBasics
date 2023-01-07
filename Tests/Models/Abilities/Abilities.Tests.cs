using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abilities;
using Tests.Helpers;

namespace Tests.Models.Abilities
{
    [TestClass]
    public class AbilitiesTests
    {
        private PropertyHelper<Ability> _propertyHelper;

        public AbilitiesTests()
        {
            _propertyHelper = new PropertyHelper<Ability>();

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
        public void HasEffect()
        {
            // Arrange
            string propertyName = "Effect";

            // Act
            var effectProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(effectProperty);
            Assert.AreEqual(effectProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasEffectCaseSensitive()
        {
            // Arrange
            string propertyName = "effect";

            // Act
            var effectPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var effectPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(effectPropertyCaseInsensitive);
            Assert.AreEqual(effectPropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(effectPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasOverworldEffect()
        {
            // Arrange
            string propertyName = "OverworldEffect";

            // Act
            var overworldEffectProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(overworldEffectProperty);
            Assert.AreEqual(overworldEffectProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasOverworldEffectCaseSensitive()
        {
            // Arrange
            string propertyName = "effect";

            // Act
            var overworldEffectPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var overworldEffectPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(overworldEffectPropertyCaseInsensitive);
            Assert.AreEqual(overworldEffectPropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(overworldEffectPropertyCaseSensitive == null);
        }
    }
}
