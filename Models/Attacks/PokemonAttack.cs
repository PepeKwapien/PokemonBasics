using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Attacks
{
    [PrimaryKey(nameof(PokemonId), nameof(AttackId))]
    public class PokemonAttack
    {
        public Guid PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        public Guid AttackId { get; set; }
        [ForeignKey("AttackId")]
        public Attack Attack { get; set; }
        public bool ByLevelUp { get; set; }
        public bool ByTm { get; set; }
        public bool ByEgg { get; set; }
        public bool ByTutor { get; set; }
        public bool SignatureMove { get; set; }
    }
}
