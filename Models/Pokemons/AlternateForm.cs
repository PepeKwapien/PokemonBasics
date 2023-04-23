using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    public class AlternateForm : IModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OriginalId { get; set; }
        [ForeignKey(nameof(OriginalId))]
        public Pokemon Original { get; set; }
        public Guid AlternateId { get; set; }
        [ForeignKey(nameof(AlternateId))]
        public Pokemon Alternate { get; set; }
    }
}
