using Models.Enums;
using Models.Pokeballs;
using System.ComponentModel.DataAnnotations;

namespace Models.Games
{
    public class Game
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        [EnumDataType(typeof(Generations))]
        public Generations Generation { get; set; }
        [StringLength(512)]
        public Uri? Icon { get; set; }

        public ICollection<Pokeball> Pokeballs { get; set; }
        public ICollection<PokemonAvailability> PokemonAvailabilities { get; set; }
    }
}
