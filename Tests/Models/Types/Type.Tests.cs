﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Helpers;
using Models.Types;

namespace Tests.Models
{
    [TestClass]
    public class TypeTests
    {
        private PropertyHelper<PokemonType> _propertyHelper;

        public TypeTests()
        {
            _propertyHelper = new PropertyHelper<PokemonType>();
        }

        #region Properties

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
        public void HasColor()
        {
            // Arrange
            string propertyName = "Color";

            // Act
            var colorProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(colorProperty);
            Assert.AreEqual(colorProperty.PropertyType.Name, nameof(System.String));
        }

        [TestMethod]
        public void HasColorCaseSensitive()
        {
            // Arrange
            string propertyName = "color";

            // Act
            var colorPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var colorPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(colorPropertyCaseInsensitive);
            Assert.AreEqual(colorPropertyCaseInsensitive.PropertyType.Name, nameof(System.String));

            Assert.IsTrue(colorPropertyCaseSensitive == null);
        }

        [TestMethod]
        public void HasIcon()
        {
            // Arrange
            string propertyName = "Icon";

            // Act
            var iconProperty = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(iconProperty);
            Assert.AreEqual(iconProperty.PropertyType.Name, nameof(System.Uri));
        }

        [TestMethod]
        public void HasIconCaseSensitive()
        {
            // Arrange
            string propertyName = "icon";

            // Act
            var iconPropertyCaseInsensitive = _propertyHelper.GetPropertyOfNameCaseInsesitive(propertyName);
            var iconPropertyCaseSensitive = _propertyHelper.GetPropertyOfName(propertyName);

            // Assert
            Assert.IsNotNull(iconPropertyCaseInsensitive);
            Assert.AreEqual(iconPropertyCaseInsensitive.PropertyType.Name, nameof(System.Uri));

            Assert.IsTrue(iconPropertyCaseSensitive == null);
        }

        #endregion
    }
}
