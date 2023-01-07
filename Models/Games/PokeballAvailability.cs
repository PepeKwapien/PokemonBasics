using Models.Pokeballs;

namespace Models.Games
{
    public class PokeballAvailability
    {
        public Guid Id { get; private set; }
        public Guid PokeballId { get; set; }
        public Pokeball Pokeball { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
