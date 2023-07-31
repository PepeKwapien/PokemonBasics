﻿using Models.Types;
using PokemonAPI.DTO;
using PokemonAPI.Models;
using PokemonAPI.Repositories.Types;

namespace PokemonAPI.Services
{
    public class PokemonTypeService : IPokemonTypeService
    {
        private readonly IPokemonTypeRepository _pokemonTypeRepository;

        public PokemonTypeService(IPokemonTypeRepository pokemonTypeRepository)
        {
            _pokemonTypeRepository = pokemonTypeRepository;
        }

        public PokemonDefensiveCharacteristics GetDefensiveCharacteristics(string primaryTypeName, string? secondaryTypeName)
        {
            List<DamageMultiplier> combinedDefensiveCharacteristics = _pokemonTypeRepository.GetTypeDefensiveCharacteristicByName(primaryTypeName);
            List<DamageMultiplier> secondaryTypeDefensiceCharacteristics = String.IsNullOrEmpty(secondaryTypeName) ?
                new List<DamageMultiplier>() : _pokemonTypeRepository.GetTypeDefensiveCharacteristicByName(secondaryTypeName);

            combinedDefensiveCharacteristics.AddRange(secondaryTypeDefensiceCharacteristics);

            List<PokemonType> allTypes = _pokemonTypeRepository.GetAllRelevantTypes();

            PokemonDefensiveCharacteristics pdc = new PokemonDefensiveCharacteristics();

            var groupedCharacterstics = combinedDefensiveCharacteristics.ToLookup(multiplier => multiplier.Type);

            foreach(var lookupPair in groupedCharacterstics)
            {
                double finalMultiplier = 1;

                foreach(var characteristicOfSubType in groupedCharacterstics[lookupPair.Key])
                {
                    finalMultiplier *= characteristicOfSubType.Multiplier;
                }

                if (finalMultiplier == 0)
                {
                    pdc.No.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }
                else if (finalMultiplier > 0 && finalMultiplier <= 0.25)
                {
                    pdc.Quarter.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }
                else if (finalMultiplier > 0.25 && finalMultiplier <= 0.5)
                {
                    pdc.Half.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }
                else if (finalMultiplier > 0.5 && finalMultiplier <= 1)
                {
                    pdc.Neutral.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }
                else if (finalMultiplier > 1 && finalMultiplier <= 2)
                {
                    pdc.Double.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }
                else
                {
                    pdc.Quadruple.Add(PokemonTypeDto.FromPokemonType(lookupPair.Key));
                }

                allTypes.Remove(lookupPair.Key);
            }

            pdc.Neutral.AddRange(allTypes.Select(type => PokemonTypeDto.FromPokemonType(type)));

            return pdc;
        }
    }
}
