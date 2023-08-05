using Models.Pokemons;
using PokemonAPI.Models;

namespace PokemonAPI.DTOs
{
    public class PokemonGeneralDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Number { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int SpecialAttack { get; set; }
        public int Defense { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public List<AbilityDto> Abilities { get; set; } = new();
        public PokemonDefensiveCharacteristics DefensiveRelations { get; set; }
        public PokemonTypeCharacteristics PrimaryType { get; set; }
        public PokemonTypeCharacteristics? SecondaryType { get; set; }

        public static PokemonGeneralDto FromPokemonAbilitiesAndTypes(
            Pokemon pokemon,
            List<AbilityDto> abilities,
            PokemonDefensiveCharacteristics defensiveRelations,
            PokemonTypeCharacteristics primaryTypeCharacteristic,
            PokemonTypeCharacteristics? secondaryTypeCharacteristic)
        {
            PokemonGeneralDto pgdto = new()
            {
                Name = pokemon.Name,
                Image = pokemon.Sprite,
                HP = pokemon.HP,
                Attack = pokemon.Attack,
                Defense = pokemon.Defense,
                SpecialAttack = pokemon.SpecialAttack,
                SpecialDefense= pokemon.SpecialDefense,
                Speed = pokemon.Speed,
                Abilities = abilities,
                DefensiveRelations = defensiveRelations,
                PrimaryType = primaryTypeCharacteristic,
                SecondaryType = secondaryTypeCharacteristic
            };

            int? nationalNumber = pokemon.PokemonEntries.FirstOrDefault(entry => entry.Pokedex.Name.Equals("national", StringComparison.OrdinalIgnoreCase))?.Number;
            pgdto.Number = nationalNumber;

            return pgdto;
        }
    }
}
