using PokemonAPI.DTO;
using System.Drawing;

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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PokemonDefensiveCharacteristics))
            {
                return base.Equals(obj);
            }
            else
            {
                PokemonDefensiveCharacteristics pdc = obj as PokemonDefensiveCharacteristics;
                return
                    No.All(pdc.No.Contains) &&
                    Quarter.All(pdc.Quarter.Contains) &&
                    Half.All(pdc.Half.Contains) &&
                    Neutral.All(pdc.Neutral.Contains) &&
                    Double.All(pdc.Double.Contains) &&
                    Quadruple.All(pdc.Quadruple.Contains);
            }
        }
    }
}
