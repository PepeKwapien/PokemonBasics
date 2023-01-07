using Models.Types;

namespace Models.Pokemons
{
    public class Pokemon
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public PokemonType PrimaryTypeId { get; set; }
        public PokemonType PrimaryType { get; set; }
        public Guid? SecondaryTypeId { get; set; }
        public PokemonType? SecondaryType { get; set; }
        public int DexNumber { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Generation { get; set; }
        public Uri? Image { get; set; }
    }
}
