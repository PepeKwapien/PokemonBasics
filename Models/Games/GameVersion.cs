using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Games
{
    public class GameVersion : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}
