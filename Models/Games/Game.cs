using Models.Enums;
using Models.Generations;
using Models.Pokeballs;
using Models.Pokedexes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Games
{
    public class Game : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]
        [StringLength(64)]
        public string PrettyName { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }
        public Regions? MainRegion { get; set; }

        public ICollection<Pokeball> Pokeballs { get; set; }
        public ICollection<Pokedex> Pokedexes { get; set; }
        public ICollection<GameVersion> GameVersions { get; set; }
    }
}
