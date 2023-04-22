using DataAccess;
using ExternalApiHandler.DTOs;
using ExternalApiHandler.Helpers;
using Logger;
using Models.Enums;
using Models.Games;
using Models.Generations;

namespace ExternalApiHandler.Mappers.Games
{
    public class GameMapper : Mapper<Game>
    {
        private readonly ILogger _logger;
        private List<GamesDto> _games;
        private List<GenerationDto> _generations;

        public GameMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
        }

        public override List<Game> Map()
        {
            List<Game> games = new List<Game>();

            foreach(GamesDto gameDto in _games)
            {
                // Set up shared game fields
                Generation generation = EntityFinderHelper.FindGenerationByDtoName(_dbContext, gameDto.VersionGroup.generation.name, _generations);
                Regions[]? regions = gameDto.VersionGroup.regions?.Select(region => EnumHelper.GetEnumValueFromKey<Regions>(region.name)).ToArray();
                Regions? mainRegion = null;

                if(regions != null && regions.Length == 1)
                {
                    mainRegion = regions[0];
                }
                else if(regions != null && regions.Length > 1)
                {
                    mainRegion = regions[regions.Length-1];
                }

                foreach(VersionDto versionDto in gameDto.Versions)
                {
                    string name = LanguageVersionHelper.FindEnglishVersion(versionDto.names).name;

                    games.Add(new Game
                    {
                        Name = name,
                        MainRegion = mainRegion,
                        GenerationId = generation.Id,
                        Generation = generation,
                    });
                    _logger.Debug($"Mapped game {name} from generation {generation.Name} placed in a region {(mainRegion != null ? mainRegion : "Unknown")}");
                }
            }

            foreach (var game in _dbContext.Games)
            {
                _logger.Debug($"Removing game {game.Name}");
                _dbContext.Games.Remove(game);
            }

            _dbContext.Games.AddRange(games);
            _dbContext.SaveChanges();
            _logger.Success($"Saved {games.Count} games");

            return games;
        }

        public void SetUp(List<GamesDto> games, List<GenerationDto> generations)
        {
            _games = games;
            _generations = generations;
        }
    }
}
