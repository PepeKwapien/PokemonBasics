using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Types;
using Moq;
using PokemonAPI.DTO;
using PokemonAPI.Models;
using PokemonAPI.Repositories;
using PokemonAPI.Services;
using System.Collections.Generic;
using Tests.TestHelpers;

namespace Tests.PokemonAPI.Services
{
    [TestClass]
    public class PokemonTypeServiceTests
    {
        private Mock<IPokemonDatabaseContext> _databaseContext;
        private IPokemonTypeRepository _typeRepository;
        private IPokemonTypeService _service;
        private PokemonType grass;
        private PokemonType water;
        private PokemonType fire;
        private PokemonType electric;
        private PokemonType flying;
        private PokemonType normal;
        private PokemonType fighting;
        private PokemonType ground;
        private PokemonType rock;
        private PokemonType dark;
        private PokemonType steel;
        private PokemonType psychic;
        private PokemonType fairy;
        private PokemonType poison;
        private PokemonType ghost;
        private PokemonType ice;
        private PokemonType bug;
        private PokemonType dragon;

        [TestInitialize]
        public void Initialize()
        {
            _databaseContext = new Mock<IPokemonDatabaseContext>();

            grass = new PokemonType()
            {
                Name = "Grass",
                Color = "Green"
            };
            water = new PokemonType()
            {
                Name = "Water",
                Color = "Blue"
            };
            fire = new PokemonType()
            {
                Name = "Fire",
                Color = "Red"
            };
            electric = new PokemonType()
            {
                Name = "Electric",
                Color = "Yellow"
            };
            flying = new PokemonType()
            {
                Name = "Flying",
                Color = "Cyan"
            };
            normal = new PokemonType()
            {
                Name = "Normal",
                Color = "Beige"
            };
            fighting = new PokemonType()
            {
                Name = "Fighting",
                Color = "Maroon"
            };
            ground = new PokemonType()
            {
                Name = "Ground",
                Color = "Brown"
            };
            rock = new PokemonType()
            {
                Name = "Rock",
                Color = "Gray"
            };
            dark = new PokemonType()
            {
                Name = "Dark",
                Color = "Black"
            };
            steel = new PokemonType()
            {
                Name = "Steel",
                Color = "Steel"
            };
            psychic = new PokemonType()
            {
                Name = "Psychic",
                Color = "Purple"
            };
            fairy = new PokemonType()
            {
                Name = "Fairy",
                Color = "Pink"
            };
            poison = new PokemonType()
            {
                Name = "Poison",
                Color = "Purple"
            };
            ghost = new PokemonType()
            {
                Name = "Ghost",
                Color = "Indigo"
            };
            ice = new PokemonType()
            {
                Name = "Ice",
                Color = "Light Blue"
            };
            bug = new PokemonType()
            {
                Name = "Bug",
                Color = "Lime Green"
            };
            dragon = new PokemonType()
            {
                Name = "Dragon",
                Color = "Dark Purple"
            };

            List<PokemonType> types = new List<PokemonType>()
            {
                grass,
                water,
                fire,
                electric,
                flying,
                normal,
                fighting,
                ground,
                rock,
                dark,
                steel,
                psychic,
                fairy,
                poison,
                ghost,
                ice,
                bug,
                dragon
            };

            List<DamageMultiplier> multiplier = new List<DamageMultiplier>()
            {
                new DamageMultiplier()
                {
                    Type = electric,
                    Against = grass,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = grass,
                    Against = grass,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = ground,
                    Against = grass,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = water,
                    Against = grass,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = bug,
                    Against = grass,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = fire,
                    Against = grass,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = flying,
                    Against = grass,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = ice,
                    Against = grass,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = poison,
                    Against = grass,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = poison,
                    Against = ground,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = rock,
                    Against = ground,
                    Multiplier = 0.5
                },
                new DamageMultiplier()
                {
                    Type = grass,
                    Against = ground,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = ice,
                    Against = ground,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = water,
                    Against = ground,
                    Multiplier = 2
                },
                new DamageMultiplier()
                {
                    Type = electric,
                    Against = ground,
                    Multiplier = 0
                },
            };

            var typesSet = PokemonDbSetHelper.SetUpDbSetMock<PokemonType>(types);
            _databaseContext.Setup(db => db.Types).Returns(typesSet.Object);
            var damageMultiplierSet = PokemonDbSetHelper.SetUpDbSetMock<DamageMultiplier>(multiplier);
            _databaseContext.Setup(db => db.DamageMultipliers).Returns(damageMultiplierSet.Object);

            _typeRepository = new PokemonTypeRepository(_databaseContext.Object);
            _service = new PokemonTypeService(_typeRepository);
        }

        [TestMethod]
        public void GetDefensiveCharacteristics_GetCorrectlyForSingleType()
        {
            // Arrange
            PokemonDefensiveCharacteristics expected = new PokemonDefensiveCharacteristics()
            {
                No = new List<PokemonTypeDto>(),
                Quarter = new List<PokemonTypeDto>(),
                Half = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(electric),
                    PokemonTypeDto.FromPokemonType(grass),
                    PokemonTypeDto.FromPokemonType(ground),
                    PokemonTypeDto.FromPokemonType(water),
                },
                Neutral = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(fairy),
                    PokemonTypeDto.FromPokemonType(ghost),
                    PokemonTypeDto.FromPokemonType(rock),
                    PokemonTypeDto.FromPokemonType(fighting),
                    PokemonTypeDto.FromPokemonType(psychic),
                    PokemonTypeDto.FromPokemonType(dark),
                    PokemonTypeDto.FromPokemonType(steel),
                    PokemonTypeDto.FromPokemonType(dragon),
                    PokemonTypeDto.FromPokemonType(normal),
                },
                Double = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(bug),
                    PokemonTypeDto.FromPokemonType(fire),
                    PokemonTypeDto.FromPokemonType(flying),
                    PokemonTypeDto.FromPokemonType(ice),
                    PokemonTypeDto.FromPokemonType(poison),
                },
                Quadruple = new List<PokemonTypeDto>()
            };

            // Act
            var result = _service.GetDefensiveCharacteristics("grass");

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetDefensiveCharacteristics_GetCorrectlyForDoubleType()
        {
            // Arrange
            PokemonDefensiveCharacteristics expected = new PokemonDefensiveCharacteristics()
            {
                No = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(electric),
                },
                Quarter = new List<PokemonTypeDto>(),
                Half = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(rock),
                    PokemonTypeDto.FromPokemonType(ground),
                },
                Neutral = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(water),
                    PokemonTypeDto.FromPokemonType(grass),
                    PokemonTypeDto.FromPokemonType(fairy),
                    PokemonTypeDto.FromPokemonType(ghost),
                    PokemonTypeDto.FromPokemonType(fighting),
                    PokemonTypeDto.FromPokemonType(psychic),
                    PokemonTypeDto.FromPokemonType(dark),
                    PokemonTypeDto.FromPokemonType(steel),
                    PokemonTypeDto.FromPokemonType(dragon),
                    PokemonTypeDto.FromPokemonType(normal),
                    PokemonTypeDto.FromPokemonType(poison),
                },
                Double = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(bug),
                    PokemonTypeDto.FromPokemonType(fire),
                    PokemonTypeDto.FromPokemonType(flying),
                },
                Quadruple = new List<PokemonTypeDto>()
                {
                    PokemonTypeDto.FromPokemonType(ice),
                }
            };

            // Act
            var result = _service.GetDefensiveCharacteristics("grass", "ground");

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
