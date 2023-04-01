using Models.Games;
using Models.Generations;
using System.ComponentModel.DataAnnotations;

namespace Models.Pokeballs
{
    public class Pokeball
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public ICollection<Generation> Generations { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
