using Models.Types;

namespace PokemonAPI.DTO
{
    public class PokemonTypeDto
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public static PokemonTypeDto FromPokemonType(PokemonType pokemonType)
        {
            return new PokemonTypeDto
            {
                Name = pokemonType.Name,
                Color = pokemonType.Color,
            };
        }
    }
}
