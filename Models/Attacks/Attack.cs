using Models.Types;

namespace Models.Attacks
{
    public class Attack
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public Type Type { get; set; }
        public string Category { get; set; }
        public string? SpecialEffect { get; set; }
    }
}
