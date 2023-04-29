using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    public class RegionalVariant : IModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OriginalId { get; set; }
        [ForeignKey(nameof(OriginalId))]
        public Pokemon Original { get; set; }
        public Guid VariantId { get; set; }
        [ForeignKey(nameof(VariantId))]
        public Pokemon Variant { get; set; }
    }
}
