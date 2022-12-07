namespace Models.Types
{
    public class DamageMultiplier
    {
        public Guid Id { get; private set; }
        public Type Type { get; set; }
        public Type Against { get; set; }
        public int Multiplier { get; set; }
    }
}
