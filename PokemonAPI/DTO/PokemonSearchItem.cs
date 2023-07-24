using Models.Pokemons;

namespace PokemonAPI.DTO
{
    public class PokemonSearchItem
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Number { get; set; }

        public static PokemonSearchItem FromPokemon(Pokemon pokemon)
        {
            PokemonSearchItem item = new PokemonSearchItem() { Name = pokemon.Name, Image = pokemon.Sprite };

            int? nationalNumber = pokemon.PokemonEntries.FirstOrDefault(entry => entry.Pokedex.Name.ToLower() == "national")?.Number;
            item.Number = nationalNumber;

            return item;
        }
    }
}
