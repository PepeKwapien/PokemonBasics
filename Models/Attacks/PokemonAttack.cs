using Models.Pokemons;

namespace Models.Attacks
{
    public class PokemonAttack
    {
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Guid AttackId { get; set; }
        public Attack Attack { get; set; }
        public bool ByLevelUp { get; set; }
        public bool ByTm { get; set; }
        public bool ByEgg { get; set; }
        public bool ByTutor { get; set; }
    }
}
