using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    public class Evolution
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public Guid IntoId { get; set; }
        [ForeignKey(nameof(IntoId))]
        public Pokemon Into { get; set; }
        [Required]
        [StringLength(256)]
        public string Method { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
