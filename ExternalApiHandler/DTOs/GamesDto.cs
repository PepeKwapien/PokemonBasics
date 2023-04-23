namespace ExternalApiCrawler.DTOs
{
    public class GamesDto : IDto
    {
        public VersionGroupDto VersionGroup { get; set; }
        public List<VersionDto> Versions { get; set; }
    }
}
