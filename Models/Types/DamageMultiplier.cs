namespace Models.Types
{
    public class DamageMultiplier
    {
        public Guid Id { get; private set; }
        public Guid TypeId { get; set; }
        public PokemonType Type { get; set; }
        public Guid AgainstId { get; set; }
        public PokemonType Against { get; set; }
        public int Multiplier { get; set; }
    }
}
