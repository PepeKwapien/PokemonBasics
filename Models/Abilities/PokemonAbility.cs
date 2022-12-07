namespace Models.Abilities
{
    public class PokemonAbility
    {
        public Guid Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public Ability Ability { get; set; }
        public bool IsHidden { get; set; }
    }
}
