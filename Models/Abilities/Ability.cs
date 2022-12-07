namespace Models.Abilities
{
    public class Ability
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public string? OverworldEffect { get; set; }
    }
}
