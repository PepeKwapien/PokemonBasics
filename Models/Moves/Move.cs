using Models.Games;
using Models.Generations;
using Models.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Moves
{
    public class Move : IModel, IHasName
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
        public int Priority { get; set; }
        public Guid TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public PokemonType Type { get; set; }
        [StringLength(32)]
        public string Category { get; set; }
        [StringLength(256)]
        public string Effect { get; set; }
        public int? SpecialEffectChance { get; set; }
        public string Target { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }
        public ICollection<PokemonMove> PokemonMoves { get; set; }
    }
}
