using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    [PrimaryKey(nameof(OriginalId), nameof(AlternateId))]
    public class AlternateForm
    {
        public Guid OriginalId { get; set; }
        [ForeignKey(nameof(OriginalId))]
        public Pokemon Original { get; set; }
        public Guid AlternateId { get; set; }
        [ForeignKey(nameof(AlternateId))]
        public Pokemon Alternate { get; set; }
    }
}
