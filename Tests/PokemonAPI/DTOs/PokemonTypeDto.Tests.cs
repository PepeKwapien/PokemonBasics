using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using PokemonAPI.DTO;

namespace Tests.PokemonAPI.DTOs
{
    [TestClass]
    public class PokemonTypeDtoTests
    {
        [TestMethod]
        public void PokemonTypeDto_FromType()
        {
            // Arrange
            PokemonType type = new PokemonType()
            {
                Name = "Grass",
                Color = "Green"
            };
            PokemonTypeDto expectedDto = new PokemonTypeDto()
            {
                Name = "Grass",
                Color = "Green"
            };

            // Act
            PokemonTypeDto result = PokemonTypeDto.FromPokemonType(type);

            // Assert
            Assert.AreEqual(expectedDto, result);
        }
    }
}
