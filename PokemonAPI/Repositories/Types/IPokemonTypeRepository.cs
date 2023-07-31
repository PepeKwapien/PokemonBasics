﻿using Models.Types;

namespace PokemonAPI.Repositories.Types
{
    public interface IPokemonTypeRepository
    {
        List<DamageMultiplier> GetTypeCharacteristicByName(string typeName); 
    }
}
