namespace Models.Types
{
    public class Type
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; set; }
        public Uri? Icon { get; set; }
    }
}