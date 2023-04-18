namespace ExternalApiHandler.DTOs
{
    public class CountDto
    {
        public int count { get; set; }
        public string next { get; set; }
        public Url[] results { get; set; }
    }
}
