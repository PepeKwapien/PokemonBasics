using Models.Enums;
using Models.Games;
using System.ComponentModel.DataAnnotations;

namespace Models.Pokedexes
{
    public class Pokedex : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(Regions))]
        public Regions? Region { get; set; }
        [StringLength(128)]
        public string Description { get; set; }
        public ICollection<PokemonAvailability> Pokemons { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
