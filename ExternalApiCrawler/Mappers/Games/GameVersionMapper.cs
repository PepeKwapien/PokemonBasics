using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Games;

namespace ExternalApiCrawler.Mappers
{
    public class GameVersionMapper : Mapper<GameVersion>
    {
        private readonly ILogger _logger;
        private List<GamesDto> _games;

        public GameVersionMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _games = new List<GamesDto>();
        }

        public override List<GameVersion> MapToDb()
        {
            List<GameVersion> gameVersions = new List<GameVersion>();

            foreach (GamesDto gameDto in _games)
            {
                List<string> versionNames = gameDto.Versions.Select(version =>
                    LanguageVersionHelper.FindEnglishVersion(version.names)?.name ?? StringHelper.Normalize(version.name)).ToList();

                string gameName = String.Join(" & ", versionNames);

                Game game = EntityFinderHelper.FindGameBySimpleName(_dbContext.Games, gameDto.VersionGroup.name, _games, _logger);

                foreach (VersionDto versionDto in gameDto.Versions)
                {
                    string name = LanguageVersionHelper.FindEnglishVersion(versionDto.names)?.name ?? StringHelper.Normalize(versionDto.name);

                    gameVersions.Add(new GameVersion
                    {
                        GameId = game.Id,
                        Game = game,
                        Name = name,
                    });
                    _logger.Debug($"Mapped game version {name} of a game {game.PrettyName}");
                }
            }

            _dbContext.GameVersions.AddRange(gameVersions);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {gameVersions.Count} game versions");

            return gameVersions;
        }

        public void SetUp(List<GamesDto> games)
        {
            _games = games;
        }
    }
}
