using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Types
{
    
    public class DamageMultiplier : IModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        public PokemonType Type { get; set; }
        public Guid AgainstId { get; set; }
        [ForeignKey(nameof(AgainstId))]
        public PokemonType Against { get; set; }
        public double Multiplier { get; set; }
    }
}
