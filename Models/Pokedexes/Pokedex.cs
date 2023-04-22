using Models.Enums;
using Models.Games;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations;

namespace Models.Pokedexes
{
    public class Pokedex : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        [EnumDataType(typeof(Regions))]
        public string Name { get; set; }
        public Regions? Region { get; set; }
        [StringLength(128)]
        public string? Description { get; set; }
        public ICollection<Pokemon> Pokemons { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
