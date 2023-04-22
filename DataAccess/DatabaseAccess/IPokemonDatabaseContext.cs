using Microsoft.EntityFrameworkCore;
using Models.Abilities;
using Models.Games;
using Models.Generations;
using Models.Moves;
using Models.Pokeballs;
using Models.Pokedexes;
using Models.Pokemons;
using Models.Types;

namespace DataAccess
{
    public interface IPokemonDatabaseContext
    {
        // Abilities
        DbSet<Ability> Abilities { get; set; }
        DbSet<Ability> PokemonAbilities { get; set; }

        // Games
        DbSet<Game> Games { get; set; }

        // Generations
        DbSet<Generation> Generations { get; set; }

        // Moves
        DbSet<Move> Moves { get; set; }
        DbSet<PokemonMove> PokemonMoves { get; set; }

        // Pokeballs
        DbSet<Pokeball> Pokeballs { get; set; }

        // Pokedexes
        DbSet<Pokedex> Pokedexes { get; set; }
        DbSet<PokemonAvailability> PokemonAvailabilities { get; set; }

        // Pokemons
        DbSet<Pokemon> Pokemons { get; set; }
        DbSet<Evolution> Evolutions { get; set; }
        DbSet<AlternateForm> AlternateForms { get; set; }

        // Types
        DbSet<PokemonType> Types { get; set; }
        DbSet<DamageMultiplier> DamageMultipliers { get; set; }

        int SaveChanges();
    }

}
