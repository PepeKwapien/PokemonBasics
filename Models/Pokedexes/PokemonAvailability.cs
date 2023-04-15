using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokedexes
{
    [PrimaryKey(nameof(PokemonId), nameof(PokedexId))]
    public class PokemonAvailability : IModel
    {
        public Guid PokemonId { get; set; }
        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public Guid PokedexId { get; set; }
        [ForeignKey(nameof(PokedexId))]
        public Pokedex Pokedex { get; set; }
        public int Number { get; set; }
    }
}
