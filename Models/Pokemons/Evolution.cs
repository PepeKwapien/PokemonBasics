using Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    public class Evolution
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        public Guid IntoId { get; set; }
        [ForeignKey("IntoId")]
        public Pokemon Into { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public EvolutionMethods Method { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
