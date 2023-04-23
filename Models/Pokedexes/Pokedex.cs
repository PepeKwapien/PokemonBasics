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
        public ICollection<PokemonEntry> Pokemons { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
