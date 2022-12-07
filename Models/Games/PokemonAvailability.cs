using Models.Pokemon;

namespace Models.Games
{
    public class PokemonAvailability
    {
        public Guid Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public Game Game { get; set; }
        public int RegionalNumber { get; set; }
    }
}
