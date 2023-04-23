namespace ExternalApiCrawler.DTOs
{
    public class EvolutionDetail
    {
        public int? gender { get; set; }
        public Name? held_item { get; set; }
        public Name? item { get; set; }
        public Name? known_move { get; set; }
        public Name? known_move_type { get; set; }
        public Name? location { get; set; }
        public int? min_affection { get; set; }
        public int? min_beauty { get; set; }
        public int? min_happiness { get; set; }
        public int? min_level { get; set; }
        public bool needs_overworld_rain { get; set; }
        public Name? party_species { get; set; }
        public Name? party_type { get; set; }
        public int? relative_physical_stats { get; set; }
        public string? time_of_day { get; set; }
        public Name? trade_species { get; set; }
        public Name trigger { get; set; }
        public bool turn_upside_down { get; set; }
    }
}
