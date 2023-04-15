using Microsoft.EntityFrameworkCore;
using Models.Games;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Moves
{
    [PrimaryKey(nameof(PokemonId), nameof(MoveId))]
    public class PokemonMove : IModel
    {
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
