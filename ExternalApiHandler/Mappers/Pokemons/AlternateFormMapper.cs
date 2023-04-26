using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
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

        public override List<AlternateForm> Map()
        {
            List<AlternateForm> alternateForms = new List<AlternateForm>();

            foreach(PokemonSpeciesDto speciesDto in _speciesDtos)
            {
                if(speciesDto.varieties.Length < 2)
                {
                    continue;
                }

                ILookup<bool, Variety> varietyGroups = speciesDto.varieties.ToLookup(variety => variety.is_default);

                Pokemon original = null;
                List<Pokemon> alternates = new List<Pokemon>();

                foreach(var group in varietyGroups)
                {
                    if (group.Key)
                    {
                        original = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, varietyGroups[group.Key].First().pokemon.name);
                    }
                    else
                    {
                        alternates = varietyGroups[group.Key].Select(varietyDto => EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, varietyDto.pokemon.name)).ToList();
                    }
                }

                foreach(Pokemon alternate in alternates)
                {
                    alternateForms.Add(new AlternateForm
                    {
                        OriginalId = original.Id,
                        Original = original,
                        AlternateId = alternate.Id,
                        Alternate = alternate,
                    });

                    _logger.Debug($"Mapped alternate form {alternate.Name} of pokemon {original.Name}");
                }
            }

            foreach(AlternateForm alternateForm in _dbContext.AlternateForms.Include(af => af.Original).Include(af => af.Alternate))
            {
                _logger.Debug($"Removing alternate form {alternateForm.Alternate.Name} of pokemon {alternateForm.Original.Name}");
                _dbContext.AlternateForms.Remove(alternateForm);
            }

            _dbContext.AlternateForms.AddRange(alternateForms);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {alternateForms.Count} alternate forms");

            return alternateForms;
        }

        public void SetUp(List<PokemonSpeciesDto> pokemonSpecies)
        {
            _speciesDtos = pokemonSpecies;
        }
    }
}
