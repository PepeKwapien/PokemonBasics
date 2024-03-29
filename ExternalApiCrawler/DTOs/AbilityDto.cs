﻿namespace ExternalApiCrawler.DTOs
{
    public class AbilityDto : IDto, IMultiLanguageNames
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public bool is_main_series { get; set; }
        public EffectEntry[] effect_entries { get; set; }
        public Name generation { get; set; }
    }
}
