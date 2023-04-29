using Models.Moves;
using Models.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Pokemons
{
    public class Evolution : IModel
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid PokemonId { get; set; }
        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public Guid IntoId { get; set; }
        [ForeignKey(nameof(IntoId))]
        public Pokemon Into { get; set; }
        [Required]
        [StringLength(64)]
        public string Trigger { get; set; }
        public string? Gender { get; set; }
        public string? HeldItem { get; set; }
        public string? Item { get; set; }
        public Guid? KnownMoveId { get; set; }
        [ForeignKey(nameof(KnownMoveId))]
        public Move? KnownMove { get; set; }
        public Guid? KnownMoveTypeId { get; set; }
        [ForeignKey(nameof(KnownMoveTypeId))]
        public PokemonType? KnownMoveType { get; set; }
        public string? Location { get; set; }
        public int? MinAffection { get; set; }
        public int? MinBeauty { get; set; }
        public int? MinHappiness { get; set; }
        public int? MinLevel { get; set; }
        public bool OverworldRain { get; set; }
        public Guid? PartySpeciesId { get; set; }
        [ForeignKey(nameof(PartySpeciesId))]
        public Pokemon? PartySpecies { get; set; }
        public Guid? PartyTypeId { get; set; }
        [ForeignKey(nameof(PartyTypeId))]
        public PokemonType? PartyType { get; set; }
        public string? RelativePhysicalStats { get; set; }
        public string? TimeOfDay { get; set; }
        public Guid? TradeSpeciesId { get; set; }
        [ForeignKey(nameof(TradeSpeciesId))]
        public Pokemon? TradeSpecies { get; set; }
        public bool TurnUpsideDown { get; set; }
        public string? BabyItem { get; set; }
    }
}
