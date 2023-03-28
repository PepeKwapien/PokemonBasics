using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Moves
{
    [PrimaryKey(nameof(PokemonId), nameof(MoveId))]
    public class PokemonMove
    {
        public Guid PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        public Guid MoveId { get; set; }
        [ForeignKey("MoveId")]
        public Move Move { get; set; }
        public bool ByLevelUp { get; set; }
        public bool ByTm { get; set; }
        public bool ByEgg { get; set; }
        public bool ByTutor { get; set; }
        public bool SignatureMove { get; set; }
    }
}
