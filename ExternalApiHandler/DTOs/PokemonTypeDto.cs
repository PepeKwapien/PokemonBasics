﻿namespace ExternalApiHandler.DTOs
{
    public class PokemonTypeDto : IDto
    {
        public string name { get; set; }
        public NameWithLanguage[] names { get; set; }
        public TypeDamageRelations damage_relations { get; set; }
    }
}
