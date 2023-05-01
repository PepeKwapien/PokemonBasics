using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Moves;
using Models.Pokemons;
using Models.Types;

namespace ExternalApiCrawler.Mappers
{
    public class EvolutionMapper : Mapper
    {
        private readonly ILogger _logger;
        private List<PokemonSpeciesDto> _speciesDtos;
        private List<EvolutionChainDto> _evolutionChainDtos;
        private List<MoveDto> _moveDtos;
        private string[] _exceptionSpeciesEvolutions; // Insanely annoying that I have to do it this way
        private string[] _exceptionSpeciesDetails; // Insanely annoying that I have to do it this way

        public EvolutionMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _speciesDtos = new List<PokemonSpeciesDto>();
            _evolutionChainDtos = new List<EvolutionChainDto>();
            _moveDtos = new List<MoveDto>();

            _exceptionSpeciesEvolutions = new string[]
            {
                "pikachu",
                "eevee",
                "basculin",
                "darumaka",
                "rockruff"
            };

            _exceptionSpeciesDetails = new string[]
            {
                "darmanitan"
            };
        }

        public List<Evolution> Map()
        {
            List<Evolution> evolutions = new List<Evolution>();

            foreach (EvolutionChainDto evolutionChainDto in _evolutionChainDtos)
            {
                string? babyItem = StringHelper.NormalizeIfNotNull(evolutionChainDto.baby_trigger_item?.name);
                evolutions.AddRange(AnalizeEvolutionTree(evolutionChainDto.chain, babyItem));
            }

            return evolutions;
        }

        public override void MapAndSave()
        {
            List<Evolution> evolutions = Map();

            _dbContext.Evolutions.AddRange(evolutions);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {evolutions.Count} evolutions");
        }

        public void SetUp(List<PokemonSpeciesDto> species, List<EvolutionChainDto> evolutions, List<MoveDto> moves)
        {
            _speciesDtos = species;
            _evolutionChainDtos = evolutions;
            _moveDtos = moves;
        }

        private List<Evolution> AnalizeEvolutionTree(EvolvesTo startOfTheChain, string? babyItem)
        {
            List<Evolution> evolutions = new List<Evolution>();

            if(startOfTheChain.evolves_to.Length == 0)
            {
                return evolutions;
            }

            List<Pokemon> evolvingFromPokemons =
                EntityFinderHelper.FindVarietiesInPokemonSpecies(_dbContext.Pokemons, startOfTheChain.species.name, _speciesDtos, _logger);

            List<EvolutionTriggerPair> evolutionTriggerPairs = new List<EvolutionTriggerPair>();

            foreach (EvolvesTo nextEvolvesTo in startOfTheChain.evolves_to)
            {
                // Recursion
                evolutions.AddRange(AnalizeEvolutionTree(nextEvolvesTo, babyItem));

                if (nextEvolvesTo.evolution_details.Length == 0)
                {
                    _logger.Warn($"Evolution into {nextEvolvesTo.species.name} has no details");
                    nextEvolvesTo.evolution_details = new EvolutionDetail[]
                    {
                        new EvolutionDetail
                        {
                            trigger = new Name
                            {
                                name = ""
                            }
                        }
                    };
                }

                List<Pokemon> evolvingIntoPokemons =
                    EntityFinderHelper.FindVarietiesInPokemonSpecies(_dbContext.Pokemons, nextEvolvesTo.species.name, _speciesDtos, _logger);

                int numberOfEvolutionsInto = evolvingIntoPokemons.Count;
                int numberOfDetails = nextEvolvesTo.evolution_details.Length;

                if (numberOfEvolutionsInto != numberOfDetails &&
                    numberOfDetails != 1 &&
                    numberOfEvolutionsInto != 1 &&
                    !_exceptionSpeciesDetails.Contains(nextEvolvesTo.species.name)) // Unclear how to pair details and varieties
                {
                    ExceptionHelper.LogAndThrow<Exception>(
                        $"Unclear expected behavior for species {nextEvolvesTo.species.name} with {numberOfEvolutionsInto} varieties and {numberOfDetails} evolution methods",
                        _logger);
                }
                else if(numberOfEvolutionsInto*numberOfDetails == 0)
                {
                    ExceptionHelper.LogAndThrow<Exception>(
                        $"Inconsistent data somewhere. Species {nextEvolvesTo.species.name}. Number of varieties: {numberOfEvolutionsInto}. Number of details {numberOfDetails}",
                        _logger);
                }

                if (_exceptionSpeciesDetails.Contains(nextEvolvesTo.species.name))
                {
                    evolutionTriggerPairs.AddRange(HandleExceptionsInEvolutionDetails(nextEvolvesTo.species.name, evolvingIntoPokemons, nextEvolvesTo.evolution_details));
                }
                else
                {
                    int max = Math.Max(numberOfEvolutionsInto, numberOfDetails);
                    for (int i = 0; i < max; i++)
                    {
                        evolutionTriggerPairs.Add(new EvolutionTriggerPair
                        {
                            pokemon = evolvingIntoPokemons[i % numberOfEvolutionsInto],
                            detail = nextEvolvesTo.evolution_details[i % numberOfDetails]
                        });
                    }
                }
            } // foreach (EvolvesTo nextEvolvesTo in startOfTheChain.evolves_to)

            int numberOfBaseForms = evolvingFromPokemons.Count;
            int numberOfEvolutionPairs = evolutionTriggerPairs.Count;

            // Annoying exceptions that I am too tired to handle in a unified way - I tried, you should have seen my notes
            if (_exceptionSpeciesEvolutions.Contains(startOfTheChain.species.name))
            {
                evolutions.AddRange(HandleExceptionsInEvolutionSpecies(startOfTheChain.species.name, evolvingFromPokemons, evolutionTriggerPairs, babyItem));
            }
            // 1-1, n-n (regional variants), 1-n (multiple evolutions), n-2n (e.g. slowpoke)
            else if (numberOfBaseForms <= numberOfEvolutionPairs && numberOfEvolutionPairs % numberOfBaseForms == 0)
            {
                for (int i = 0; i < numberOfEvolutionPairs; i++)
                {
                    EvolutionTriggerPair currentPair = evolutionTriggerPairs[i];
                    evolutions.Add(CreateEvolution(evolvingFromPokemons[i % numberOfBaseForms], currentPair.pokemon, currentPair.detail, babyItem));
                }
            }
            // Regional evolution - add evolution only to the latest variant
            else if (numberOfBaseForms > numberOfEvolutionPairs && numberOfEvolutionPairs == 1)
            {
                evolutions.Add(CreateEvolution(evolvingFromPokemons[numberOfBaseForms - 1], evolutionTriggerPairs[0].pokemon, evolutionTriggerPairs[0].detail, babyItem));
            }
            // Unclear what to do
            else
            {
                ExceptionHelper.LogAndThrow<Exception>(
                    $"Unclear expected behavior for species {startOfTheChain.species.name} while evolving. Number of base forms {numberOfBaseForms}, number of evolutions {numberOfEvolutionPairs}",
                    _logger);
            }

            return evolutions;
        }

        private List<EvolutionTriggerPair> HandleExceptionsInEvolutionDetails(string speciesName, List<Pokemon> varieties, EvolutionDetail[] details)
        {
            List<EvolutionTriggerPair> evolutionTriggerPairs = new List<EvolutionTriggerPair>();

            switch (speciesName)
            {
                case "darmanitan":
                    for(int i = 0; i < varieties.Count; i++)
                    {
                        evolutionTriggerPairs.Add(new EvolutionTriggerPair
                        {
                            pokemon = varieties[i],
                            detail = details[i / details.Length]
                        });
                    }
                    break;
            };

            return evolutionTriggerPairs;
        }

        private List<Evolution> HandleExceptionsInEvolutionSpecies(string speciesName, List<Pokemon> evolvesFrom, List<EvolutionTriggerPair> evolutionTriggerPairs, string? babyItem)
        {
            List<Evolution> evolutions= new List<Evolution>();

            switch (speciesName)
            {
                case "pikachu":
                case "eevee":
                    for (int i = 0; i < evolutionTriggerPairs.Count; i++)
                    {
                        var currentPair = evolutionTriggerPairs[i];
                        evolutions.Add(CreateEvolution(evolvesFrom[0], currentPair.pokemon, currentPair.detail, babyItem));
                    }
                    break;
                case "basculin":
                    for (int i = 0; i < evolutionTriggerPairs.Count; i++)
                    {
                        var currentPair = evolutionTriggerPairs[i];
                        evolutions.Add(CreateEvolution(evolvesFrom[evolvesFrom.Count - 1], currentPair.pokemon, currentPair.detail, babyItem));
                    }
                    break;
                case "darumaka":
                    for (int i = 0; i < evolutionTriggerPairs.Count; i++)
                    {
                        var currentPair = evolutionTriggerPairs[i];
                        evolutions.Add(CreateEvolution(evolvesFrom[i / evolvesFrom.Count], currentPair.pokemon, currentPair.detail, babyItem));
                    }
                    break;
                case "rockruff":
                    for(int i = 0; i < 2; i++)
                    {
                        var currentPair = evolutionTriggerPairs[i];
                        evolutions.Add(CreateEvolution(evolvesFrom[0], currentPair.pokemon, currentPair.detail, babyItem));
                    }
                    evolutions.Add(CreateEvolution(evolvesFrom[1], evolutionTriggerPairs[2].pokemon, evolutionTriggerPairs[2].detail, babyItem));
                    break;
            }

            return evolutions;
        }

        private Evolution CreateEvolution(Pokemon pokemon, Pokemon evolutionPokemon, EvolutionDetail detail, string? babyItem)
        {
            Pokemon? partyPokemon = detail.party_species != null ?
                EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, detail.party_species.name, _logger) : null;
            Pokemon? tradePokemon = detail.trade_species != null ?
                EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, detail.trade_species.name, _logger) : null;
            PokemonType? knownMoveType = detail.known_move_type != null ?
                EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, detail.known_move_type.name, _logger) : null;
            PokemonType? partyType = detail.party_type != null ?
                EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, detail.party_type.name, _logger) : null;
            Move? knownMove = detail.known_move != null ?
                EntityFinderHelper.FindEntityByDtoName(_dbContext.Moves, detail.known_move.name, _moveDtos, _logger) : null;
            string trigger = StringHelper.Normalize(detail.trigger.name);

            Evolution evolution = new Evolution()
            {
                PokemonId = pokemon.Id,
                Pokemon = pokemon,
                IntoId = evolutionPokemon.Id,
                Into = evolutionPokemon,
                Trigger = trigger,
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

            _logger.Debug($"Mapped evolution from {pokemon.Name} to {evolutionPokemon.Name} by method {trigger}");

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
