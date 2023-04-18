namespace ExternalApiHandler.Options
{
    public class ExternalApiOptions
    {
        public string BaseUrl { get; set; }
        public string ClientName { get; set; }
        public string PokemonTypePath { get; set; }
        public string PokemonAbilityPath { get; set; }
        public string PokemonMovePath { get; set; }
        public string GenerationPath { get; set; }
        public string ItemCategoryPath { get; set; }
        public string[] ItemCategoriesForPokeballs { get; set; }
        public string VersionGroupPath { get; set; }
        public string PokedexPath { get; set; }
        public string PokemonPath { get; set; }
        public string PokemonSpeciesPath { get; set; }
        public string EvolutionChainPath { get; set; }
    }
}
