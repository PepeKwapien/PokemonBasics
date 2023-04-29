using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Moves;
using Models.Pokemons;
using Models.Types;

namespace ExternalApiCrawler.Mappers.Pokemons
{
    public class EvolutionMapper : Mapper<Evolution>
    {
        private readonly ILogger _logger;
        private List<PokemonSpeciesDto> _speciesDtos;
        private List<EvolutionChainDto> _evolutionChainDtos;
        private List<MoveDto> _moveDtos;

        public EvolutionMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _speciesDtos = new List<PokemonSpeciesDto>();
            _evolutionChainDtos = new List<EvolutionChainDto>();
            _moveDtos = new List<MoveDto>();
        }

        public override List<Evolution> Map()
        {
            List<Evolution> evolutions = new List<Evolution>();

            foreach (EvolutionChainDto evolutionChainDto in _evolutionChainDtos)
            {
                string? babyItem = StringHelper.NormalizeIfNotNull(evolutionChainDto.baby_trigger_item?.name);
                evolutions.AddRange(AnalizeEvolutionTree(evolutionChainDto.chain, babyItem));
            }

            foreach (Evolution evolution in _dbContext.Evolutions.Include(e => e.Into).Include(e => e.Pokemon))
            {
                _logger.Debug($"Removing evolution from {evolution.Pokemon.Name} to {evolution.Into.Name}");
                _dbContext.Evolutions.Remove(evolution);
            }

            _dbContext.Evolutions.AddRange(evolutions);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {evolutions.Count} evolutions");

            return evolutions;
        }

        public void SetUp(List<PokemonSpeciesDto> species, List<EvolutionChainDto> evolutions, List<MoveDto> moves)
        {
            _speciesDtos = species;
            _evolutionChainDtos = evolutions;
            _moveDtos = moves;
        }

        private List<Evolution> AnalizeEvolutionTree(EvolvesTo evolvesTo, string? babyItem)
        {
            List<Evolution> evolutions = new List<Evolution>();

            if(evolvesTo.evolves_to.Length == 0)
            {
                return evolutions;
            }

            List<Pokemon> basePokemons = EntityFinderHelper.FindVarietiesInPokemonSpecies(_dbContext.Pokemons, evolvesTo.species.name, _speciesDtos);
            List<EvolutionTriggerPair> evolutionTriggerPairs = new List<EvolutionTriggerPair>(); 

            foreach (EvolvesTo innerEvolvesTo in evolvesTo.evolves_to)
            {
                // recursion
                evolutions.AddRange(AnalizeEvolutionTree(innerEvolvesTo, babyItem));

                List<Pokemon> evolutionPokemons = EntityFinderHelper.FindVarietiesInPokemonSpecies(_dbContext.Pokemons, innerEvolvesTo.species.name, _speciesDtos);

                int numberOfEvolutions = evolutionPokemons.Count;
                int numberOfDetails = innerEvolvesTo.evolution_details.Length;

                if (numberOfEvolutions != numberOfDetails && numberOfDetails != 1 && numberOfEvolutions != 1) // unclear how to pair details and varieties
                {
                    _logger.Error($"Unclear expected behavior for species {innerEvolvesTo.species.name} with {numberOfEvolutions} varieties and {numberOfDetails} evolution methods");
                    throw new Exception($"Unclear expected behavior for species {innerEvolvesTo.species.name} with {numberOfEvolutions} varieties and {numberOfDetails} evolution methods");
                }

                int max = Math.Max(numberOfEvolutions, numberOfDetails);
                for(int i = 0; i < max; i++)
                {
                    evolutionTriggerPairs.Add(new EvolutionTriggerPair
                    {
                        pokemon = evolutionPokemons[i% numberOfEvolutions],
                        detail = innerEvolvesTo.evolution_details[i% numberOfDetails]
                    });
                }
            }

            int numberOfBaseForms = basePokemons.Count;
            int numberOfEvolutionPairs = evolutionTriggerPairs.Count;

            // 1-1, n-n (regional variants), 1-n (multiple evolutions), n-2n (e.g. slowpoke)
            if(numberOfBaseForms <= numberOfEvolutionPairs && numberOfEvolutionPairs%numberOfBaseForms == 0)
            {
                for(int i = 0; i < numberOfEvolutionPairs; i++)
                {
                    EvolutionTriggerPair currentPair = evolutionTriggerPairs[i];
                    evolutions.Add(CreateEvolution(basePokemons[i%numberOfBaseForms], currentPair.pokemon, currentPair.detail, babyItem));
                }
            }
            // regional evolution - add evolution only to the latest variant
            else if(numberOfBaseForms > numberOfEvolutionPairs && numberOfEvolutionPairs == 1)
            {
                evolutions.Add(CreateEvolution(basePokemons[numberOfBaseForms - 1], evolutionTriggerPairs[0].pokemon, evolutionTriggerPairs[0].detail, babyItem));
            }
            // unclear what to do
            else
            {
                _logger.Error($"Unclear expected behavior for species {evolvesTo} while evoling. Number of base forms {numberOfBaseForms}, number of evolutions {numberOfEvolutionPairs}");
                throw new Exception($"Unclear expected behavior for species {evolvesTo} while evoling. Number of base forms {numberOfBaseForms}, number of evolutions {numberOfEvolutionPairs}");
            }

            return evolutions;
        }

        private Evolution CreateEvolution(Pokemon pokemon, Pokemon evolutionPokemon, EvolutionDetail detail, string? babyItem)
        {
            Pokemon? partyPokemon = detail.party_species != null ? EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, detail.party_species.name) : null;
            Pokemon? tradePokemon = detail.trade_species != null ? EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, detail.trade_species.name) : null;
            PokemonType? knownMoveType = detail.known_move_type != null ? EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, detail.known_move_type.name) : null;
            PokemonType? partyType = detail.party_type != null ? EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, detail.party_type.name) : null;
            Move? knownMove = detail.known_move != null ? EntityFinderHelper.FindEntityByDtoName(_dbContext.Moves, detail.known_move.name, _moveDtos) : null;

            Evolution evolution = new Evolution()
            {
                PokemonId = pokemon.Id,
                Pokemon = pokemon,
                IntoId = evolutionPokemon.Id,
                Into = evolutionPokemon,
                Trigger = StringHelper.Normalize(detail.trigger.name),
                Gender = ConvertGender(detail.gender),
                HeldItem = StringHelper.NormalizeIfNotNull(detail.held_item?.name),
                Item = StringHelper.NormalizeIfNotNull(detail.item?.name),
                KnownMoveId = knownMove?.Id,
                KnownMove = knownMove,
                KnownMoveTypeId = knownMoveType?.Id,
                KnownMoveType = knownMoveType,
                Location = StringHelper.NormalizeIfNotNull(detail.location?.name),
                MinAffection = detail.min_affection,
                MinBeauty = detail.min_beauty,
                MinHappiness = detail.min_happiness,
                MinLevel = detail.min_level,
                OverworldRain = detail.needs_overworld_rain,
                PartySpeciesId = partyPokemon?.Id,
                PartySpecies = partyPokemon,
                PartyTypeId = partyType?.Id,
                PartyType = partyType,
                RelativePhysicalStats = ConvertRelativePhysicalStats(detail.relative_physical_stats),
                TimeOfDay = StringHelper.NormalizeIfNotNull(detail.time_of_day),
                TradeSpeciesId = tradePokemon?.Id,
                TradeSpecies = tradePokemon,
                TurnUpsideDown = detail.turn_upside_down,
                BabyItem = babyItem
            };

            _logger.Debug($"Mapped evolution from {pokemon.Name} to {evolutionPokemon.Name}");

            return evolution;
        }

        private string? ConvertGender(int? gender)
        {
            if(gender == null)
            {
                return null;
            }

            return gender == 1 ? "Female" : "Male";
        }

        private string? ConvertRelativePhysicalStats(int? statsRatio)
        {
            if(statsRatio > 0)
            {
                return "Attack higher than Defence";
            }
            else if(statsRatio < 0)
            {
                return "Defence higher than Attack";
            }
            else if(statsRatio == 0)
            {
                return "Attack equal to Defence";
            }
            else
            {
                return null;
            }
        }

        private class EvolutionTriggerPair
        {
            public Pokemon pokemon;
            public EvolutionDetail detail;
        }
    }
}
