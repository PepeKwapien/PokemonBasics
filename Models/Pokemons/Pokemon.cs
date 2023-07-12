using Models.Abilities;
using Models.Moves;
using Models.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Generations;
using Models.Pokedexes;

namespace Models.Pokemons
{
    public class Pokemon : IModel, IHasName
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public Guid PrimaryTypeId { get; set; }
        [ForeignKey(nameof(PrimaryTypeId))]
        public PokemonType PrimaryType { get; set; }
        public Guid? SecondaryTypeId { get; set; }
        [ForeignKey(nameof(SecondaryTypeId))]
        public PokemonType? SecondaryType { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string? Habitat { get; set; }
        public string EggGroups { get; set; }
        public string Genera { get; set; }
        public bool HasGenderDifferences { get; set; }
        public bool Baby { get; set; }
        public bool Legendary { get; set; }
        public bool Mythical { get; set; }
        public string? Shape { get; set; }
        public string? Sprite { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }

        public ICollection<PokemonEntry> PokemonAvailabilities { get; set; }
        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
        public ICollection<PokemonMove> PokemonMoves { get; set; }
    }
}
