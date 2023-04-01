namespace ExternalApiHandler.DTOs
{
    internal class EvolutionDetail
    {
        public string? gender { get; set; }
        public string? held_item { get; set; }
        public string? item { get; set; }
        public string? known_move { get; set; }
        public string? known_move_type { get; set; }
        public string? location { get; set; }
        public string? min_affection { get; set; }
        public string? min_beauty { get; set; }
        public string? min_happiness { get; set; }
        public int min_level { get; set; }
        public bool needs_overworld_rain { get; set; }
        public string? party_species { get; set; }
        public string? party_type { get; set; }
        public string? relative_physical_stats { get; set; }
        public string? time_of_day { get; set; }
        public string? trade_species { get; set; }
        public Name trigger { get; set; }
        public bool turn_upside_down { get; set; }
    }
}
