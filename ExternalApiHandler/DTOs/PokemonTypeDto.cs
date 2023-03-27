namespace ExternalApiHandler.DTOs
{
    internal class PokemonTypeDto : IDto
    {
        public string name { get; set; }
        public TypeDamageRelations damage_relations { get; set; }
    }
}
