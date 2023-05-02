using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Pokemons;

namespace ExternalApiCrawler.Mappers
{
    public class AlternateFormMapper : Mapper
    {
        private readonly ILogger _logger;
        private List<PokemonSpeciesDto> _speciesDtos;

        public AlternateFormMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _speciesDtos = new List<PokemonSpeciesDto>();
        }

        public List<AlternateForm> Map()
        {
            List<AlternateForm> alternateForms = new List<AlternateForm>();

            foreach (PokemonSpeciesDto speciesDto in _speciesDtos)
            {
                if (speciesDto.varieties.Length < 2)
                {
                    continue;
                }

                ILookup<bool, Variety> varietyGroups = speciesDto.varieties.ToLookup(variety => variety.is_default);

                Pokemon original = null;
                List<Pokemon> variants = new List<Pokemon>();

                foreach (var group in varietyGroups)
                {
                    if (group.Key)
                    {
                        original = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, varietyGroups[group.Key].First().pokemon.name, _logger);
                    }
                    else
                    {
                        variants = varietyGroups[group.Key].Select(varietyDto =>
                            EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, varietyDto.pokemon.name, _logger)).ToList();
                    }
                }

                foreach (Pokemon variant in variants)
                {
                    alternateForms.Add(new AlternateForm
                    {
                        OriginalId = original.Id,
                        Original = original,
                        VariantId = variant.Id,
                        Variant = variant,
                    });

                    _logger.Debug($"Mapped alternate form {variant.Name} of pokemon {original.Name}");
                }
            }

            return alternateForms;
        }

        public override void MapAndSave()
        {
            List<AlternateForm> regionalVariants = Map();

            _dbContext.AlternateForms.AddRange(regionalVariants);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {regionalVariants.Count} regional variants");
        }

        public void SetUp(List<PokemonSpeciesDto> pokemonSpecies)
        {
            _speciesDtos = pokemonSpecies;
        }
    }
}
