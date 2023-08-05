using PokemonAPI.Models;

namespace PokemonAPI.Services
{
    public interface IPokemonTypeService
    {
        PokemonTypeCharacteristics GetTypeCharacteristic(string typeName);
        PokemonDefensiveCharacteristics GetDefensiveCharacteristics(string primaryTypeName, string? secondaryTypeName = null);
    }
}
