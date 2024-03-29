﻿namespace ExternalApiCrawler.DTOs
{
    public class VersionDto : IDto, IMultiLanguageNames
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public Name version_group { get; set; }
    }
}
