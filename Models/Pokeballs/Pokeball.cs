using Models.Games;
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
        [StringLength(512)]
        public Uri? Icon { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
