using Models.Types;

namespace PokemonAPI.DTOs
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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PokemonTypeDto))
            {
                return base.Equals(obj);
            }
            else
            {
                PokemonTypeDto ptd = obj as PokemonTypeDto;
                return Name == ptd.Name && Color == ptd.Color;
            }
        }
    }
}
