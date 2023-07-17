using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Mappers;
using ExternalApiCrawler.Requesters;
using Logger;

namespace ExternalApiCrawler
{
    public class Orchestrator : IOrchestrator
    {
        #region Injected Services
        private readonly IPokemonDatabaseContext _databaseContext;
        private readonly IEvolutionsRequester _evolutionsRequester;
        private readonly IGamesRequester _gamesRequester;
        private readonly IGenerationsRequester _generationsRequester;
        private readonly IPokeballsRequester _pokeballsRequester;
        private readonly IPokedexesRequester _pokedexesRequester;
        private readonly IPokemonsRequester _pokemonsRequester;
        private readonly IPokemonAbilitiesRequester _pokemonAbilitiesRequester;
        private readonly IPokemonMovesRequester _pokemonMovesRequester;
        private readonly IPokemonSpeciesRequester _pokemonSpeciesRequester;
        private readonly IPokemonTypesRequester _pokemonTypesRequester;
        private readonly AbilityMapper _abilityMapper;
        private readonly PokemonAbilityMapper _pokemonAbilityMapper;
        private readonly GameMapper _gameMapper;
        private readonly GameVersionMapper _gameVersionMapper;
        private readonly GenerationMapper _generationMapper;
        private readonly MoveMapper _moveMapper;
        private readonly PokemonMoveMapper _pokemonMoveMapper;
        private readonly PokeballMapper _pokeballMapper;
        private readonly PokedexMapper _pokedexMapper;
        private readonly PokemonEntryMapper _pokemonEntryMapper;
        private readonly EvolutionMapper _evolutionMapper;
        private readonly PokemonMapper _pokemonMapper;
        private readonly AlternateFormMapper _regionalVariantMapper;
        private readonly DamageMultiplierMapper _damageMultiplierMapper;
        private readonly PokemonTypeMapper _pokemonTypeMapper;
        private readonly ILogger _logger;
        #endregion
        #region DTOs
        private List<EvolutionChainDto> _evolutionChainDtos;
        private List<GamesDto> _gamesDtos;
        private List<GenerationDto> _generationDtos;
        private List<PokeballDto> _pokeballDtos;
        private List<PokedexDto> _pokedexDtos;
        private List<PokemonDto> _pokemonDtos;
        private List<AbilityDto> _pokemonAbilitiesDtos;
        private List<MoveDto> _pokemonMovesDtos;
        private List<PokemonSpeciesDto> _pokemonSpeciesDtos;
        private List<PokemonTypeDto> _pokemonTypesDtos;
        #endregion

        public Orchestrator(
            IPokemonDatabaseContext databaseContext,
            IEvolutionsRequester evolutionsRequester,
            IGamesRequester gamesRequester,
            IGenerationsRequester generationsRequester,
            IPokeballsRequester pokeballsRequester,
            IPokedexesRequester pokedexesRequester,
            IPokemonsRequester pokemonsRequester,
            IPokemonAbilitiesRequester pokemonAbilitiesRequester,
            IPokemonMovesRequester pokemonMovesRequester,
            IPokemonSpeciesRequester pokemonSpeciesRequester,
            IPokemonTypesRequester pokemonTypesRequester,
            AbilityMapper abilityMapper,
            PokemonAbilityMapper pokemonAbilityMapper,
            GameMapper gameMapper,
            GameVersionMapper gameVersionMapper,
            GenerationMapper generationMapper,
            MoveMapper moveMapper,
            PokemonMoveMapper pokemonMoveMapper,
            PokeballMapper pokeballMapper,
            PokedexMapper pokedexMapper,
            PokemonEntryMapper pokemonEntryMapper,
            EvolutionMapper evolutionMapper,
            PokemonMapper pokemonMapper,
            AlternateFormMapper regionalVariantMapper,
            DamageMultiplierMapper damageMultiplierMapper,
            PokemonTypeMapper pokemonTypeMapper,
            ILogger logger
            )
        {
            #region Assign injected services
            _databaseContext = databaseContext;
            _evolutionsRequester = evolutionsRequester;
            _gamesRequester = gamesRequester;
            _generationsRequester = generationsRequester;
            _pokeballsRequester = pokeballsRequester;
            _pokedexesRequester = pokedexesRequester;
            _pokemonsRequester = pokemonsRequester;
            _pokemonAbilitiesRequester = pokemonAbilitiesRequester;
            _pokemonMovesRequester = pokemonMovesRequester;
            _pokemonSpeciesRequester = pokemonSpeciesRequester;
            _pokemonTypesRequester = pokemonTypesRequester;
            _abilityMapper = abilityMapper;
            _pokemonAbilityMapper = pokemonAbilityMapper;
            _gameMapper = gameMapper;
            _gameVersionMapper = gameVersionMapper;
            _generationMapper = generationMapper;
            _moveMapper = moveMapper;
            _pokemonMoveMapper = pokemonMoveMapper;
            _pokeballMapper = pokeballMapper;
            _pokedexMapper = pokedexMapper;
            _pokemonEntryMapper = pokemonEntryMapper;
            _evolutionMapper = evolutionMapper;
            _pokemonMapper = pokemonMapper;
            _regionalVariantMapper = regionalVariantMapper;
            _damageMultiplierMapper = damageMultiplierMapper;
            _pokemonTypeMapper = pokemonTypeMapper;
            _logger = logger;
            #endregion
            #region Empty lists
            _evolutionChainDtos = new List<EvolutionChainDto>();
            _gamesDtos = new List<GamesDto>();
            _generationDtos = new List<GenerationDto>();
            _pokeballDtos = new List<PokeballDto>();
            _pokedexDtos = new List<PokedexDto>();
            _pokemonDtos = new List<PokemonDto>();
            _pokemonAbilitiesDtos = new List<AbilityDto>();
            _pokemonMovesDtos = new List<MoveDto>();
            _pokemonSpeciesDtos = new List<PokemonSpeciesDto>();
            _pokemonTypesDtos = new List<PokemonTypeDto>();
            #endregion
        }

        public async Task<bool> Start()
        {
            if(
                (await GetDtos()) &&
                ClearContext() &&
                SetUpMappers() &&
                ChainAndStartMappers())
            {
                _logger.Info("Successfully orchestrated database population. Congratulations!");
                return true;
            }
            else
            {
                _logger.Warn("Operation of populating the database was not successful. Check the logs to find more information");
                return false;
            }
                
        }

        private async Task<bool> GetDtos()
        {
            try
            {
                _evolutionChainDtos = await _evolutionsRequester.GetCollection();
                _gamesDtos = await _gamesRequester.GetCollection();
                _generationDtos = await _generationsRequester.GetCollection();
                _pokeballDtos = await _pokeballsRequester.GetCollection();
                _pokedexDtos = await _pokedexesRequester.GetCollection();
                _pokemonDtos = await _pokemonsRequester.GetCollection();
                _pokemonAbilitiesDtos = await _pokemonAbilitiesRequester.GetCollection();
                _pokemonMovesDtos = await _pokemonMovesRequester.GetCollection();
                _pokemonSpeciesDtos = await _pokemonSpeciesRequester.GetCollection();
                _pokemonTypesDtos = await _pokemonTypesRequester.GetCollection();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace ?? "No stack trace");
                return false;
            }

            return true;
        }

        private bool ClearContext()
        {
            try
            {
                _databaseContext.Evolutions.RemoveRange(_databaseContext.Evolutions);
                _databaseContext.AlternateForms.RemoveRange(_databaseContext.AlternateForms);
                _databaseContext.PokemonEntries.RemoveRange(_databaseContext.PokemonEntries);
                _databaseContext.PokemonAbilities.RemoveRange(_databaseContext.PokemonAbilities);
                _databaseContext.PokemonMoves.RemoveRange(_databaseContext.PokemonMoves);
                _databaseContext.Games.RemoveRange(_databaseContext.Games);
                _databaseContext.Pokedexes.RemoveRange(_databaseContext.Pokedexes);
                _databaseContext.Pokemons.RemoveRange(_databaseContext.Pokemons);
                _databaseContext.Abilities.RemoveRange(_databaseContext.Abilities);
                _databaseContext.Moves.RemoveRange(_databaseContext.Moves);
                _databaseContext.Pokeballs.RemoveRange(_databaseContext.Pokeballs);
                _databaseContext.Generations.RemoveRange(_databaseContext.Generations);
                _databaseContext.DamageMultipliers.RemoveRange(_databaseContext.DamageMultipliers);
                _databaseContext.Types.RemoveRange(_databaseContext.Types);

                _databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                }
                _logger.Error(ex.StackTrace ?? "No stack trace");
                return false;
            }

            return true;
        }

        private bool SetUpMappers()
        {
            _abilityMapper.SetUp(_pokemonAbilitiesDtos, _generationDtos);
            _pokemonAbilityMapper.SetUp(_pokemonDtos, _pokemonAbilitiesDtos);
            _gameMapper.SetUp(_gamesDtos, _pokedexDtos, _generationDtos);
            _gameVersionMapper.SetUp(_gamesDtos);
            _generationMapper.SetUp(_generationDtos);
            _moveMapper.SetUp(_pokemonMovesDtos, _generationDtos);
            _pokemonMoveMapper.SetUp(_pokemonDtos, _pokemonMovesDtos, _gamesDtos);
            _pokeballMapper.SetUp(_pokeballDtos, _generationDtos);
            _pokedexMapper.SetUp(_pokedexDtos);
            _pokemonEntryMapper.SetUp(_pokedexDtos, _pokemonSpeciesDtos);
            _evolutionMapper.SetUp(_pokemonSpeciesDtos, _evolutionChainDtos, _pokemonMovesDtos);
            _pokemonMapper.SetUp(_pokemonDtos, _pokemonSpeciesDtos, _generationDtos);
            _regionalVariantMapper.SetUp(_pokemonSpeciesDtos);
            _damageMultiplierMapper.SetUp(_pokemonTypesDtos);
            _pokemonTypeMapper.SetUp(_pokemonTypesDtos);

            return true;
        }

        private bool ChainAndStartMappers()
        {
            _pokemonTypeMapper
                .Next(_damageMultiplierMapper)
                .Next(_generationMapper)
                .Next(_pokeballMapper)
                .Next(_moveMapper)
                .Next(_abilityMapper)
                .Next(_pokemonMapper)
                .Next(_pokedexMapper)
                .Next(_gameMapper)
                .Next(_gameVersionMapper)
                .Next(_pokemonMoveMapper)
                .Next(_pokemonAbilityMapper)
                .Next(_pokemonEntryMapper)
                .Next(_regionalVariantMapper)
                .Next(_evolutionMapper);

            try
            {
                _pokemonTypeMapper.StartChain();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                if(ex.InnerException != null)
                {
                    _logger.Error(ex.InnerException.Message);
                }
                _logger.Error(ex.StackTrace ?? "No stack trace");
                return false;
            }

            return true;
        }
    }
}
