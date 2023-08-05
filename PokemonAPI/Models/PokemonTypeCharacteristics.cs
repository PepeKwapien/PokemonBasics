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
        public List<PokemonTypeDto> DoublTo { get; set; } = new();
        public List<PokemonTypeDto> DoublFrom { get; set; } = new();

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
                        FillToAndFromRelations(multiplierGroups[multiplierGroup.Key], type, pokemonTypeCharacteristics.DoublTo, pokemonTypeCharacteristics.DoublFrom);
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
