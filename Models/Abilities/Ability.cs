using System.ComponentModel.DataAnnotations;

namespace Models.Abilities
{
    public class Ability
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string Effect { get; set; }
        [StringLength(256)]
        public string? OverworldEffect { get; set; }

        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
    }
}
