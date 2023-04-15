﻿using System.ComponentModel.DataAnnotations;

namespace Models.Types
{
    public class PokemonType : IModel
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Required]
        [StringLength(16)]
        public string Color { get; set; }
    }
}