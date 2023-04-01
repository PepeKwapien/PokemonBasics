namespace ExternalApiHandler.DTOs
{
    internal class CountDto
    {
        public int count { get; set; }
        public string next { get; set; }
        public Url[] results { get; set; }
    }
}
