namespace Models.Pokemon
{
    public class AlternateForm
    {
        public Guid Id { get; set; }
        public Pokemon Original { get; set; }
        public Pokemon Alternate { get; set; }
    }
}
