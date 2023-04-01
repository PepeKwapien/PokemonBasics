using Models.Abilities;
using Models.Moves;
using Models.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Generations;
using Models.Pokedexes;

namespace Models.Pokemons
{
    public class Pokemon
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public Guid PrimaryTypeId { get; set; }
        [ForeignKey(nameof(PrimaryTypeId))]
        public PokemonType PrimaryType { get; set; }
        public Guid? SecondaryTypeId { get; set; }
        [ForeignKey(nameof(SecondaryTypeId))]
        public PokemonType? SecondaryType { get; set; }
        public int DexNumber { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Guid GenerationId { get; set; }
        [ForeignKey(nameof(GenerationId))]
        public Generation Generation { get; set; }
        [StringLength(512)]
        public Uri? Image { get; set; }

        public ICollection<PokemonAvailability> PokemonAvailabilities { get; set; }
        public ICollection<PokemonAbility> PokemonAbilities { get; set; }
        public ICollection<PokemonMove> PokemonMoves { get; set; }
    }
}
