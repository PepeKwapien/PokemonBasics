using Models.Enums;
using Models.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Moves
{
    public class Move
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public int PP { get; set; }
        public Guid TypeId { get; set; }
        public PokemonType Type { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        [EnumDataType(typeof(AttackCategories))]
        public AttackCategories Category { get; set; }
        [StringLength(128)]
        public string? SpecialEffect { get; set; }

        public ICollection<PokemonMove> PokemonMoves { get; set; }
    }
}
