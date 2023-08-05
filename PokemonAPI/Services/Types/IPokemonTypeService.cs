using PokemonAPI.Models;

namespace PokemonAPI.Services
{
    public interface IPokemonTypeService
    {
        PokemonDefensiveCharacteristics GetDefensiveCharacteristics(string primaryTypeName, string? secondaryTypeName = null);
    }
}
