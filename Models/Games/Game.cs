namespace Models.Games
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public Uri Icon { get; set; }
    }
}
