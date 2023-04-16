using DataAccess;
using ExternalApiHandler.DTOs;
using Models.Types;

namespace ExternalApiHandler.Mappers
{
    internal class DamageMultiplierMapper : Mapper<DamageMultiplier>
    {
        private List<PokemonTypeDto> _pokemonTypesDto;

        public DamageMultiplierMapper(PokemonDatabaseContext dbContext) : base(dbContext)
        {

        }

        public override List<DamageMultiplier> Map()
        {
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>();

            foreach (PokemonTypeDto typeDto in _pokemonTypesDto)
            {
                PokemonType currentType = FindTypeByNameLowercase(typeDto.name);


                if(currentType == null)
                {
                    continue;
                }

                var newDamageMultipliers = CreateAllDamageMultipliersForType(currentType, typeDto.damage_relations);
                damageMultipliers.AddRange(newDamageMultipliers);
            }

            foreach (var damageMultiplier in _dbContext.DamageMultipliers)
            {
                _dbContext.DamageMultipliers.Remove(damageMultiplier);
            }

            _dbContext.DamageMultipliers.AddRange(damageMultipliers);
            _dbContext.SaveChanges();

            return damageMultipliers;
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }

        #region Private Methods
        private PokemonType FindTypeByNameLowercase(string typeName)
        {
            return _dbContext.Types.FirstOrDefault(type => type.Name.ToLower() == typeName);
        }

        private bool DoesDamageMultiplierExist(PokemonType type, PokemonType against, double multiplyValue)
        {
            var possiblyExistingMultiplier = _dbContext.DamageMultipliers.FirstOrDefault(multiplier =>
                multiplier.TypeId == type.Id &&
                multiplier.AgainstId == against.Id &&
                multiplier.Multiplier == multiplyValue);

            return possiblyExistingMultiplier != null;
        }

        private List<DamageMultiplier> CreateDamageMultipliers(PokemonType currentType, Name[] typeRelations, double multiplyValue, bool currentIsAttacking = true)
        {
            List<DamageMultiplier> damageMultipliers= new List<DamageMultiplier>();

            foreach(Name typeRelation in typeRelations)
            {
                PokemonType typeInRelation = FindTypeByNameLowercase(typeRelation.name);

                if (typeInRelation == null)
                {
                    continue;
                }

                PokemonType attakcingType = currentIsAttacking ? currentType : typeInRelation;
                PokemonType defendingType = currentIsAttacking ? typeInRelation : currentType;

                if (DoesDamageMultiplierExist(attakcingType, defendingType, multiplyValue))
                {
                    continue;
                }

                DamageMultiplier damageMultiplier = new DamageMultiplier()
                {
                    Multiplier = multiplyValue,
                    Type = attakcingType,
                    Against = defendingType,
                };

                damageMultipliers.Add(damageMultiplier);
            }

            return damageMultipliers;
        }

        private List<DamageMultiplier> CreateAllDamageMultipliersForType(PokemonType currentType, TypeDamageRelations relations)
        {
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>();

            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.double_damage_to, 2));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.double_damage_from, 2, false));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.half_damage_to, 0.5));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.half_damage_from, 0.5, false));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.no_damage_to, 0));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.no_damage_from, 0, false));

            return damageMultipliers;
        }
        #endregion
    }
}
