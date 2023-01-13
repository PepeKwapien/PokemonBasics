using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Abilities
{
    [PrimaryKey(nameof(PokemonId), nameof(AbilityId))]
    public class PokemonAbility
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        public Guid AbilityId { get; set; }
        [ForeignKey("AbilityId")]
        public Ability Ability { get; set; }
        public bool IsHidden { get; set; } = false;
    }
}
