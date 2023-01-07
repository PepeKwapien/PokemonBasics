using Models.Pokemons;

namespace Models.Abilities
{
    public class PokemonAbility
    {
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Guid AbilityId { get; set; }
        public Ability Ability { get; set; }
        public bool IsHidden { get; set; }
    }
}
