using Models.Types;

namespace PokemonAPI.Helpers
{
    public class DamageMultiplierHelper
    {
        public static bool IsTypeAttacking(PokemonType type, DamageMultiplier multiplier)
        {
            return multiplier.Type.Name.Equals(type.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
