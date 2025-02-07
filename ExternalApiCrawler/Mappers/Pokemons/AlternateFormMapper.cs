using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Pokemons;

namespace ExternalApiCrawler.Mappers
{
    public class AlternateFormMapper : Mapper<AlternateForm>
    {
        private readonly ILogger _logger;
        private List<PokemonSpeciesDto> _speciesDtos;

        public AlternateFormMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _speciesDtos = new List<PokemonSpeciesDto>();
        }

        public override List<AlternateForm> MapToDb()
        {
            List<AlternateForm> alternateForms = new List<AlternateForm>();

            foreach (PokemonSpeciesDto speciesDto in _speciesDtos)
            {
                if (speciesDto.varieties.Length < 2)
                {
                    continue;
                }

                ILookup<bool, Variety> varietyGroups = speciesDto.varieties.ToLookup(variety => variety.is_default);

                Pokemon? original = null;
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
                                EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, varietyDto.pokemon.name, _logger))
                            .Where(pokemon => pokemon != null)
                            .ToList()!;
                    }
                }

                foreach (Pokemon variant in variants)
                {
                    alternateForms.Add(new AlternateForm
                    {
                        OriginalId = original!.Id,
                        Original = original,
                        VariantId = variant.Id,
                        Variant = variant,
                    });

                    _logger.Debug($"Mapped alternate form {variant.Name} of pokemon {original.Name}");
                }
            }

            _dbContext.AlternateForms.AddRange(alternateForms);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {alternateForms.Count} regional variants");

            return alternateForms;
        }

        public void SetUp(List<PokemonSpeciesDto> pokemonSpecies)
        {
            _speciesDtos = pokemonSpecies;
        }
    }
}
