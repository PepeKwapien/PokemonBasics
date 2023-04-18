using DataAccess;
using ExternalApiHandler.DTOs;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Types;

namespace ExternalApiHandler.Mappers
{
    public class DamageMultiplierMapper : Mapper<DamageMultiplier>
    {
        private List<PokemonTypeDto> _pokemonTypesDto;
        private readonly ILogger _logger;

        public DamageMultiplierMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
        }

        public override List<DamageMultiplier> Map()
        {
            // Have to clear relations because there is a check whether specific relation already exists
            foreach (var damageMultiplier in _dbContext.DamageMultipliers.Include(dm => dm.Type).Include(dm => dm.Against))
            {
                _logger.Debug($"Removing damage multiplier when {damageMultiplier.Type.Name} is attacking {damageMultiplier.Against.Name}");
                _dbContext.DamageMultipliers.Remove(damageMultiplier);
            }

            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>();

            foreach (PokemonTypeDto typeDto in _pokemonTypesDto)
            {
                PokemonType currentType = FindTypeByNameLowercase(typeDto.name);

                if(currentType == null)
                {
                    _logger.Warn($"No type with name {typeDto.name} was found. Skipping creating its relations");
                    continue;
                }

                var newDamageMultipliers = CreateAllDamageMultipliersForType(currentType, typeDto.damage_relations, damageMultipliers);
                damageMultipliers.AddRange(newDamageMultipliers);
            }

            _dbContext.DamageMultipliers.AddRange(damageMultipliers);
            _dbContext.SaveChanges();
            _logger.Success($"Saved {damageMultipliers.Count} damage relations");

            return damageMultipliers;
        }

        public void SetUp(List<PokemonTypeDto> pokemonTypesDto)
        {
            _pokemonTypesDto = pokemonTypesDto;
        }

        #region Private Methods
        private PokemonType FindTypeByNameLowercase(string typeName)
        {
            return _dbContext.Types.FirstOrDefault(type => type.Name.ToLower() == typeName.ToLower());
        }

        private bool DoesDamageMultiplierExist(PokemonType type, PokemonType against, double multiplyValue, List<DamageMultiplier> damageMultipliers)
        {
            var possiblyExistingMultiplier = damageMultipliers.FirstOrDefault(multiplier =>
                (multiplier.TypeId == type.Id || multiplier.Type.Name == type.Name) &&
                (multiplier.AgainstId == against.Id || multiplier.Against.Name == against.Name) &&
                multiplier.Multiplier == multiplyValue);

            return possiblyExistingMultiplier != null;
        }

        private List<DamageMultiplier> CreateDamageMultipliers(
            PokemonType currentType,
            Name[] typeRelations,
            double multiplyValue,
            bool currentIsAttacking,
            List<DamageMultiplier> allDamageMultipliers,
            List<DamageMultiplier> typeDamageMultipliers
            )
        {
            List<DamageMultiplier> damageMultipliers= new List<DamageMultiplier>();

            foreach(Name typeRelation in typeRelations)
            {
                PokemonType typeInRelation = FindTypeByNameLowercase(typeRelation.name);

                if (typeInRelation == null)
                {
                    _logger.Warn($"Unknown type {typeRelation.name} couldn't be found." +
                        $"Skipping creating relation with type {currentType.Name} where damage multiplier equals {multiplyValue}." +
                        $"Is type {currentType.Name} attacking?: {currentIsAttacking}");
                    continue;
                }

                PokemonType attakcingType = currentIsAttacking ? currentType : typeInRelation;
                PokemonType defendingType = currentIsAttacking ? typeInRelation : currentType;

                if (DoesDamageMultiplierExist(attakcingType, defendingType, multiplyValue, allDamageMultipliers) ||
                    DoesDamageMultiplierExist(attakcingType, defendingType, multiplyValue, typeDamageMultipliers) ||
                    DoesDamageMultiplierExist(attakcingType, defendingType, multiplyValue, damageMultipliers))
                {
                    _logger.Info($"Relation where {attakcingType.Name} attacks {defendingType.Name} with multiplier {multiplyValue} already exists. Skipping");
                    continue;
                }

                DamageMultiplier damageMultiplier = new DamageMultiplier()
                {
                    Multiplier = multiplyValue,
                    Type = attakcingType,
                    Against = defendingType,
                };

                damageMultipliers.Add(damageMultiplier);
                _logger.Debug($"Damage multiplier where {attakcingType.Name} attacks {defendingType.Name} with multiplier {multiplyValue} created");
            }

            return damageMultipliers;
        }

        private List<DamageMultiplier> CreateAllDamageMultipliersForType(PokemonType currentType, TypeDamageRelations relations, List<DamageMultiplier> allTypesDamageMultipliers)
        {
            List<DamageMultiplier> damageMultipliers = new List<DamageMultiplier>();

            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.double_damage_to, 2, true, allTypesDamageMultipliers, damageMultipliers));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.double_damage_from, 2, false, allTypesDamageMultipliers, damageMultipliers));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.half_damage_to, 0.5, true, allTypesDamageMultipliers, damageMultipliers));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.half_damage_from, 0.5, false, allTypesDamageMultipliers, damageMultipliers));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.no_damage_to, 0, true, allTypesDamageMultipliers, damageMultipliers));
            damageMultipliers.AddRange(CreateDamageMultipliers(currentType, relations.no_damage_from, 0, false, allTypesDamageMultipliers, damageMultipliers));

            return damageMultipliers;
        }
        #endregion
    }
}
