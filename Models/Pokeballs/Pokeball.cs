namespace Models.Pokeballs
{
    public class Pokeball
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public Uri? Icon { get; set; }
    }
}
