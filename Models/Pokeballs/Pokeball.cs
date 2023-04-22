using Models.Games;
using Models.Generations;
using System.ComponentModel.DataAnnotations;

namespace Models.Pokeballs
{
    public class Pokeball : IModel, IHasName
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Generation> Generations { get; set; }
    }
}
