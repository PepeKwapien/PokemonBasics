namespace ExternalApiHandler.DTOs
{
    internal class CountDto
    {
        public int count { get; set; }
        public string next { get; set; }
        public Result[] results { get; set; }
    }

    internal class Result
    {
        public string url { get; set; }
    }
}
