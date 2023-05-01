using Models.Generations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Abilities
{
    public class Ability : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Required]
        public string Effect { get; set; }
        public string? OverworldEffect { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }

        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
    }
}
