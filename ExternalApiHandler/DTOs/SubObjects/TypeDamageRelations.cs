namespace ExternalApiHandler.DTOs
{
    internal class TypeDamageRelations
    {
        public TypeDamageRelation[] double_damage_from { get; set; }
        public TypeDamageRelation[] double_damage_to { get; set; }
        public TypeDamageRelation[] half_damage_from { get; set; }
        public TypeDamageRelation[] half_damage_to { get; set; }
        public TypeDamageRelation[] no_damage_from { get; set; }
        public TypeDamageRelation[] no_damage_to { get; set; }
    }
}
