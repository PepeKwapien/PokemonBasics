using Models.Generations;
using Models.Pokeballs;
using Models.Pokedexes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Games
{
    public class Game : IModel
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }
        [StringLength(512)]
        public Uri? Icon { get; set; }

        public ICollection<Pokeball> Pokeballs { get; set; }
        public ICollection<Pokedex> Pokedexes { get; set; }
    }
}
