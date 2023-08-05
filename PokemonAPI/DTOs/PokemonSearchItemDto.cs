using Models.Pokemons;

namespace PokemonAPI.DTO
{
    public class PokemonSearchItemDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Number { get; set; }

        public static PokemonSearchItemDto FromPokemon(Pokemon pokemon)
        {
            PokemonSearchItemDto item = new PokemonSearchItemDto() { Name = pokemon.Name, Image = pokemon.Sprite };

            int? nationalNumber = pokemon.PokemonEntries.FirstOrDefault(entry => entry.Pokedex.Name.Equals("national", StringComparison.OrdinalIgnoreCase))?.Number;
            item.Number = nationalNumber;

            return item;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != typeof(PokemonSearchItemDto))
            {
                return base.Equals(obj);
            }
            else
            {
                PokemonSearchItemDto psi = obj as PokemonSearchItemDto;
                return Name == psi.Name && Image == psi.Image && Number == psi.Number;
            }
        }
    }
}
