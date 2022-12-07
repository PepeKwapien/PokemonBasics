using Models.Pokeballs;

namespace Models.Games
{
    public class PokeballAvailability
    {
        public Guid Id { get; private set; }
        public Pokeball Pokeball { get; set; }
        public Game Game { get; set; }
    }
}
