﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Types
{
    [PrimaryKey(nameof(TypeId), nameof(AgainstId))]
    public class DamageMultiplier
    {
        public Guid TypeId { get; set; }
        [ForeignKey("TypeId")]
        public PokemonType Type { get; set; }
        public Guid AgainstId { get; set; }
        [ForeignKey("AgainstId")]
        public PokemonType Against { get; set; }
        public double Multiplier { get; set; }
    }
}