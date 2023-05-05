using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Enums;
using Models.Games;
using Models.Generations;
using Models.Pokedexes;

namespace ExternalApiCrawler.Mappers
{
    public class GameMapper : Mapper<Game>
    {
        private readonly ILogger _logger;
        private List<GamesDto> _games;
        private List<PokedexDto> _pokedexDtos;
        private List<GenerationDto> _generations;

        public GameMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _games = new List<GamesDto>();
            _pokedexDtos = new List<PokedexDto>();
            _generations = new List<GenerationDto>();
        }

        public override List<Game> MapToDb()
        {
            List<Game> games = new List<Game>();

            foreach (GamesDto gameDto in _games)
            {
                // Set up shared game fields
                Generation generation = EntityFinderHelper.FindEntityByDtoName(_dbContext.Generations, gameDto.VersionGroup.generation.name, _generations, _logger);
                Regions[]? regions = gameDto.VersionGroup.regions?.Select(region => EnumHelper.GetEnumValueFromKey<Regions>(region.name, _logger)).ToArray();
                Regions? mainRegion = null;

                List<Pokedex> pokedexes = new List<Pokedex>();
                foreach (var pokedexName in gameDto.VersionGroup.pokedexes)
                {
                    try
                    {
                        Pokedex pokedex = EntityFinderHelper.FindEntityByDtoName(_dbContext.Pokedexes, pokedexName.name, _pokedexDtos, _logger);
                        pokedexes.Add(pokedex);
                    }
                    catch (Exception ex)
                    {
                        // TODO Remove in the future once the S/V DLCs are out
                        // Unlucky inconsistent restful API
                        continue;
                    }
                }

                if (regions != null && regions.Length == 1)
                {
                    mainRegion = regions[0];
                }
                else if (regions != null && regions.Length > 1)
                {
                    mainRegion = regions[regions.Length - 1];
                }

                foreach (VersionDto versionDto in gameDto.Versions)
                {
                    string name = LanguageVersionHelper.FindEnglishVersion(versionDto.names)?.name ?? StringHelper.Normalize(versionDto.name);

                    games.Add(new Game
                    {
                        Name = name,
                        MainRegion = mainRegion,
                        GenerationId = generation.Id,
                        Generation = generation,
                        Pokedexes = pokedexes,
                    });
                    _logger.Debug($"Mapped game {name} from generation {generation.Name} placed in a region {(mainRegion != null ? mainRegion : "Unknown")}");
                }
            }

            _dbContext.Games.AddRange(games);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {games.Count} games");

            return games;
        }

        public void SetUp(List<GamesDto> games, List<PokedexDto> pokedexes, List<GenerationDto> generations)
        {
            _games = games;
            _pokedexDtos = pokedexes;
            _generations = generations;
        }
    }
}
