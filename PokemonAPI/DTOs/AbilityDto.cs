using Models.Abilities;
using PokemonAPI.DTO;

namespace PokemonAPI.DTOs
{
    public class AbilityDto
    {
        public string Name { get; set; }
        public string Effect { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(AbilityDto))
            {
                return base.Equals(obj);
            }
            else
            {
                AbilityDto ad = obj as AbilityDto;
                return Name == ad.Name;
            }
        }

        public static AbilityDto FromAbility(Ability ability)
        {
            return new AbilityDto()
            {
                Name = ability.Name,
                Effect = ability.Effect
            };
        }
        public static AbilityDto FromPokemonAbility(PokemonAbility pokemonAbility)
        {
            return FromAbility(pokemonAbility.Ability);
        }        
    }
}
