namespace Models.Pokemons
{
    public class AlternateForm
    {
        public Guid Id { get; private set; }
        public Guid OriginalId { get; set; }
        public Pokemon Original { get; set; }
        public Guid AlternateId { get; set; }
        public Pokemon Alternate { get; set; }
    }
}
