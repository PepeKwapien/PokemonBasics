using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Games;
using Models.Moves;
using Models.Pokemons;

namespace ExternalApiCrawler.Mappers
{
    public class PokemonMoveMapper : Mapper<PokemonMove>
    {
        private readonly ILogger _logger;
        private List<PokemonDto> _pokemonDtos;
        private List<MoveDto> _moveDtos;
        private List<GamesDto> _gamesDtos;

        public PokemonMoveMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokemonDtos = new List<PokemonDto>();
            _moveDtos = new List<MoveDto>();
            _gamesDtos = new List<GamesDto>();
        }

        public override List<PokemonMove> Map()
        {
            List<PokemonMove> pokemonMoves= new List<PokemonMove>();

            foreach(PokemonDto pokemonDto in _pokemonDtos)
            {
                Pokemon pokemon = EntityFinderHelper.FindPokemonByName(_dbContext.Pokemons, pokemonDto.name);

                foreach(InnerPokemonMove innerPokemonMove in pokemonDto.moves)
                {
                    Move move = EntityFinderHelper.FindEntityByDtoName(_dbContext.Moves, innerPokemonMove.move.name, _moveDtos);

                    foreach (VersionGroupDetails versionGroupDetails in innerPokemonMove.version_group_details)
                    {
                        string method = StringHelper.Normalize(versionGroupDetails.move_learned_method.name);
                        List<Game> games = EntityFinderHelper.FindGamesByVersionGroupName(_dbContext.Games, versionGroupDetails.version_group.name, _gamesDtos);

                        foreach (Game game in games)
                        {
                            pokemonMoves.Add(new PokemonMove
                            {
                                PokemonId = pokemon.Id,
                                Pokemon = pokemon,
                                MoveId = move.Id,
                                Move = move,
                                GameId = game.Id,
                                Game = game,
                                Method = method,
                                MinimalLevel = versionGroupDetails.level_learned_at
                            });
                            _logger.Debug($"Mapped move {move.Name} for pokemon {pokemon.Name} learned by {method} in game {game.Name}");
                        } // End loop adding object for each group
                    } // End loop finding games
                } // End loop finding moves
            } // End loop finding pokemons

            foreach (PokemonMove pokemonMove in _dbContext.PokemonMoves.Include(pm => pm.Move).Include(pm => pm.Pokemon).Include(pm => pm.Game))
            {
                _logger.Debug($"Removing move {pokemonMove.Move.Name} for pokemon {pokemonMove.Pokemon.Name} in game {pokemonMove.Game.Name}");
                _dbContext.PokemonMoves.Remove(pokemonMove);
            }

            _dbContext.PokemonMoves.AddRange(pokemonMoves);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokemonMoves.Count} pokemon moves");

            return pokemonMoves;
        }

        public void SetUp(List<PokemonDto> pokemons, List<MoveDto> moves, List<GamesDto> games)
        {
            _pokemonDtos = pokemons;
            _moveDtos = moves;
            _gamesDtos = games;
        }
    }
}
