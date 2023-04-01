namespace ExternalApiHandler.DTOs
{
    internal class GamesDto : IDto
    {
        public VersionGroupDto VersionGroup { get; set; }
        public List<VersionDto> Versions { get; set; }
    }
}
