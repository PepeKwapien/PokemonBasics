using Microsoft.EntityFrameworkCore;
using Models.Abilities;
using Models.Attacks;
using Models.Games;
using Models.Pokeballs;
using Models.Pokemons;
using Models.Types;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class PokemonDatabaseContext : DbContext
    {
        // Types
        public DbSet<PokemonType> Types { get; set; }
        public DbSet<DamageMultiplier> DamageMultiplier { get; set; }

        // Abilities
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<PokemonAbility> PokemonAbilities { get; set; }

        // Attacks
        public DbSet<Attack> Attacks { get; set; }
        public DbSet<PokemonAttack> PokemonAttacks { get; set; }

        // Pokeballs
        public DbSet<Pokeball> Pokeballs { get; set; }

        // Games
        public DbSet<Game> Games { get; set; }
        public DbSet<PokeballAvailability> PokeballsAvailability { get; set; }
        public DbSet<PokemonAvailability> PokemonAvailabilities { get; set; }

        // Pokemons
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<AlternateForm> AlternateForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultDatabase"].ConnectionString);
        }
    }
}
