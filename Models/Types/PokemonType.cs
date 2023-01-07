namespace Models.Types
{
    public class PokemonType
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Uri? Icon { get; set; }
    }
}