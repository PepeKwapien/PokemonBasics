using Models.Pokemons;

namespace Models.Games
{
    public class PokemonAvailability
    {
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int RegionalNumber { get; set; }
    }
}
