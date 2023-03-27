namespace ExternalApiHandler.DTOs
{
    internal class PokemonTypeDto : IDto
    {
        public string name { get; set; }
        public TypeDamageRelations damage_relations { get; set; }
    }

    internal class TypeDamageRelations
    {
        public TypeDamageRelation[] double_damage_from { get; set; }
        public TypeDamageRelation[] double_damage_to { get; set; }
        public TypeDamageRelation[] half_damage_from { get; set; }
        public TypeDamageRelation[] half_damage_to { get; set; }
        public TypeDamageRelation[] no_damage_from { get; set; }
        public TypeDamageRelation[] no_damage_to { get; set; }
    }

    internal class TypeDamageRelation
    {
        public string name { get; set; }
    }
}
