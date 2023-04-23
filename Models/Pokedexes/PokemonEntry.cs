using Microsoft.EntityFrameworkCore;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokedexes
{
    public class PokemonEntry : IModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PokemonId { get; set; }
        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public Guid PokedexId { get; set; }
        [ForeignKey(nameof(PokedexId))]
        public Pokedex Pokedex { get; set; }
        public int Number { get; set; }
    }
}
