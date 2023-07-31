using PokemonAPI.DTO;

namespace PokemonAPI.Models
{
    public class PokemonDefensiveCharacteristics
    {
        public PokemonDefensiveCharacteristics()
        {
            No = new List<PokemonTypeDto>();
            Quarter = new List<PokemonTypeDto>();
            Half = new List<PokemonTypeDto>();
            Neutral = new List<PokemonTypeDto>();
            Double = new List<PokemonTypeDto>();
            Quadruple = new List<PokemonTypeDto>();
        }
        public List<PokemonTypeDto> No { get; set; }
        public List<PokemonTypeDto> Quarter { get; set; }
        public List<PokemonTypeDto> Half { get; set; }
        public List<PokemonTypeDto> Neutral { get; set; }
        public List<PokemonTypeDto> Double { get; set; }
        public List<PokemonTypeDto> Quadruple { get; set; }
    }
}
