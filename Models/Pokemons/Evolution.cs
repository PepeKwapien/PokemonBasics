namespace Models.Pokemons
{
    public class Evolution
    {
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Guid IntoId { get; set; }
        public Pokemon Into { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
    }
}
