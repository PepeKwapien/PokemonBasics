namespace ExternalApiHandler.DTOs
{
    internal class VersionDto
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public Name version_group { get; set; }
    }
}
