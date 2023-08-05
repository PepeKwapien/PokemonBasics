using Models.Types;
using PokemonAPI.DTOs;
using PokemonAPI.Helpers;

namespace PokemonAPI.Models
{
    public class PokemonTypeCharacteristics
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<PokemonTypeDto> NoTo { get; set; } = new();
        public List<PokemonTypeDto> NoFrom { get; set; } = new();
        public List<PokemonTypeDto> HalfTo { get; set; } = new();
        public List<PokemonTypeDto> HalfFrom { get; set; } = new();
        public List<PokemonTypeDto> DoubleTo { get; set; } = new();
        public List<PokemonTypeDto> DoubleFrom { get; set; } = new();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PokemonTypeCharacteristics))
            {
                return base.Equals(obj);
            }
            else
            {
                PokemonTypeCharacteristics ptc = obj as PokemonTypeCharacteristics;
                return
                    Name == ptc.Name &&
                    Color == ptc.Color &&
                    NoTo.Count == ptc.NoTo.Count && NoTo.All(ptc.NoTo.Contains) &&
                    NoFrom.Count == ptc.NoFrom.Count && NoFrom.All(ptc.NoFrom.Contains) &&
                    HalfTo.Count == ptc.HalfTo.Count && HalfTo.All(ptc.HalfTo.Contains) &&
                    HalfFrom.Count == ptc.HalfFrom.Count && HalfFrom.All(ptc.HalfFrom.Contains) &&
                    DoubleTo.Count == ptc.DoubleTo.Count && DoubleTo.All(ptc.DoubleTo.Contains) &&
                    DoubleFrom.Count == ptc.DoubleFrom.Count && DoubleFrom.All(ptc.DoubleFrom.Contains);
            }
        }

        public static PokemonTypeCharacteristics FromPokemonTypeAndDamageMultipliers(PokemonType type, List<DamageMultiplier> multipliers)
        {
            PokemonTypeCharacteristics pokemonTypeCharacteristics = new()
            {
                Name = type.Name,
                Color = type.Color
            };

            var multiplierGroups = multipliers.ToLookup(multiplier => multiplier.Multiplier);

            foreach (var multiplierGroup in multiplierGroups)
            {
                switch (multiplierGroup.Key)
                {
                    case 0:
                        FillToAndFromRelations(multiplierGroups[multiplierGroup.Key], type, pokemonTypeCharacteristics.NoTo, pokemonTypeCharacteristics.NoFrom);
                        break;
                    case 2:
                        FillToAndFromRelations(multiplierGroups[multiplierGroup.Key], type, pokemonTypeCharacteristics.DoubleTo, pokemonTypeCharacteristics.DoubleFrom);
                        break;
                    default:
                        FillToAndFromRelations(multiplierGroups[multiplierGroup.Key], type, pokemonTypeCharacteristics.HalfTo, pokemonTypeCharacteristics.HalfFrom);
                        break;
                }
            }

            return pokemonTypeCharacteristics;
        }

        private static void FillToAndFromRelations(IEnumerable<DamageMultiplier> damageMultipliers, PokemonType type, List<PokemonTypeDto> listTo, List<PokemonTypeDto> listFrom)
        {
            foreach (var multiplier in damageMultipliers)
            {
                if(multiplier.Type.Name == type.Name && multiplier.Against.Name == type.Name)
                {
                    listTo.Add(PokemonTypeDto.FromPokemonType(multiplier.Against));
                    listFrom.Add(PokemonTypeDto.FromPokemonType(multiplier.Type));
                    continue;
                }
                bool isTypeAttacking = DamageMultiplierHelper.IsTypeAttacking(type, multiplier);

                if(isTypeAttacking)
                {
                    listTo.Add(PokemonTypeDto.FromPokemonType(multiplier.Against));
                }
                else
                {
                    listFrom.Add(PokemonTypeDto.FromPokemonType(multiplier.Type));
                }
            }
        }
    }
}
