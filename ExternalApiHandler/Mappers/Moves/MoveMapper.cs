using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Microsoft.EntityFrameworkCore;
using Models.Generations;
using Models.Moves;
using Models.Types;

namespace ExternalApiCrawler.Mappers
{
    public class MoveMapper : Mapper
    {
        private readonly ILogger _logger;
        private List<MoveDto> _movesDtos;
        private List<GenerationDto> _generationDtos;

        public MoveMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _movesDtos = new List<MoveDto>();
            _generationDtos= new List<GenerationDto>();
        }

        public List<Move> Map()
        {
            List<Move> moves = new List<Move>();

            foreach (var moveDto in _movesDtos)
            {
                PokemonType type = EntityFinderHelper.FindTypeByNameCaseInsensitive(_dbContext.Types, moveDto.type.name, _logger);
                Generation generation = EntityFinderHelper.FindEntityByDtoName(_dbContext.Generations, moveDto.generation.name, _generationDtos, _logger);
                string name = LanguageVersionHelper.FindEnglishVersion(moveDto.names)?.name ?? StringHelper.Normalize(moveDto.name);
                string effect = LanguageVersionHelper.FindEnglishVersion(moveDto.effect_entries)?.effect ?? "-";
                string target = StringHelper.Normalize(moveDto.target.name);
                string category = StringHelper.Normalize(moveDto.damage_class.name);

                moves.Add(new Move
                {
                    Name = name,
                    Power = moveDto.power,
                    Accuracy = moveDto.accuracy,
                    PP = moveDto.pp,
                    Priority = moveDto.priority,
                    TypeId = type.Id,
                    Type = type,
                    Category = category,
                    Effect = effect,
                    SpecialEffectChance = moveDto.effect_chance,
                    Target = target,
                    GenerationId = generation.Id,
                    Generation = generation,
                });

                _logger.Debug($"Mapped move {name}");
            }

            return moves;
        }

        public override void MapAndSave()
        {
            List<Move> moves = Map();

            _dbContext.Moves.AddRange(moves);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {moves.Count} moves");
        }

        public void SetUp(List<MoveDto> moves, List<GenerationDto> generations)
        {
            _movesDtos = moves;
            _generationDtos = generations;
        }
    }
}
