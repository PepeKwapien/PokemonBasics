using Models.Abilities;
using Models.Enums;
using Models.Games;
using Models.Moves;
using Models.Pokeballs;
using Models.Pokemons;
using System.ComponentModel.DataAnnotations;

namespace Models.Generations
{
    public class Generation
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EnumDataType(typeof(Regions))]
        public Regions Regions { get; set; }
        public ICollection<Ability> Abilities { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<Move> Moves { get; set; }
        public ICollection<Pokeball> Pokeballs { get; set;}
        public ICollection<Pokemon> Pokemons { get; set; }

    }
}
