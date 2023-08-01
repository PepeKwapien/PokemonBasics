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

            int? nationalNumber = pokemon.PokemonEntries.FirstOrDefault(entry => entry.Pokedex.Name.Equals("national", StringComparison.OrdinalIgnoreCase))?.Number;
            item.Number = nationalNumber;

            return item;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if(obj.GetType() != typeof(PokemonSearchItem))
            {
                return base.Equals(obj);
            }
            else
            {
                PokemonSearchItem psi = obj as PokemonSearchItem;
                return Name == psi.Name && Image == psi.Image && Number == psi.Number;
            }
        }
    }
}
