using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Games
{
    [PrimaryKey(nameof(PokemonId), nameof(GameId))]
    public class PokemonAvailability
    {
        public Guid PokemonId { get; set; }
        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public int RegionalNumber { get; set; }
    }
}
