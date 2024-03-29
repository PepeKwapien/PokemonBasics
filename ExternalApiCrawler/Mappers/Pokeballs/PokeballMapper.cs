﻿using DataAccess;
using ExternalApiCrawler.DTOs;
using ExternalApiCrawler.Helpers;
using Logger;
using Models.Generations;
using Models.Pokeballs;

namespace ExternalApiCrawler.Mappers
{
    public class PokeballMapper : Mapper<Pokeball>
    {
        private readonly ILogger _logger;
        private List<PokeballDto> _pokeballDtos;
        private List<GenerationDto> _generationDtos;

        public PokeballMapper(IPokemonDatabaseContext dbContext, ILogger logger) : base(dbContext)
        {
            _logger = logger;
            _pokeballDtos = new List<PokeballDto>();
            _generationDtos = new List<GenerationDto>();
        }

        public override List<Pokeball> MapToDb()
        {
            List<Pokeball> pokeballs = new List<Pokeball>();

            foreach (PokeballDto pokeballDto in _pokeballDtos)
            {
                string name = LanguageVersionHelper.FindEnglishVersion(pokeballDto.names).name;
                string description = LanguageVersionHelper.FindEnglishVersion(pokeballDto.effect_entries)?.effect ?? "-";
                List<Generation> generations = new List<Generation>();
                foreach (var generationInDto in pokeballDto.game_indices)
                {
                    generations.Add(EntityFinderHelper.FindEntityByDtoName(_dbContext.Generations, generationInDto.generation.name, _generationDtos, _logger));
                }

                pokeballs.Add(new Pokeball
                {
                    Name = name,
                    Description = description,
                    Generations = generations,
                    Sprite = pokeballDto.sprites.@default
                });

                _logger.Debug($"Mapped pokeball {name}");
            }

            _dbContext.Pokeballs.AddRange(pokeballs);
            _dbContext.SaveChanges();
            _logger.Info($"Saved {pokeballs.Count} pokeballs");

            return pokeballs;
        }

        public void SetUp(List<PokeballDto> pokeballs, List<GenerationDto> generations)
        {
            _pokeballDtos = pokeballs;
            _generationDtos = generations;
        }
    }
}
