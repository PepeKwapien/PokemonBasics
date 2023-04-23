using Models.Games;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Moves
{
    public class PokemonMove : IModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PokemonId { get; set; }
        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public Guid MoveId { get; set; }
        [ForeignKey(nameof(MoveId))]
        public Move Move { get; set; }
        public string Method { get; set; }
        public int MinimalLevel { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}
