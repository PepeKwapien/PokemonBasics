namespace Models.Pokemon
{
    public class Evolution
    {
        public Guid Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public Pokemon Into { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
    }
}
