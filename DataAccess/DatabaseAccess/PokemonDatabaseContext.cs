using Microsoft.EntityFrameworkCore;
using Models.Abilities;
using Models.Moves;
using Models.Games;
using Models.Pokeballs;
using Models.Pokemons;
using Models.Types;
using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class PokemonDatabaseContext : DbContext
    {
        #region DbSets
        // Abilities
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<PokemonAbility> PokemonAbilities { get; set; }

        // Attacks
        public DbSet<Move> Moves { get; set; }
        public DbSet<PokemonMove> PokemonMoves { get; set; }

        // Games
        public DbSet<Game> Games { get; set; }
        public DbSet<PokemonAvailability> PokemonAvailabilities { get; set; }

        // Pokeballs
        public DbSet<Pokeball> Pokeballs { get; set; }

        // Pokemons
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<AlternateForm> AlternateForms { get; set; }

        // Types
        public DbSet<PokemonType> Types { get; set; }
        public DbSet<DamageMultiplier> DamageMultiplier { get; set; }
        #endregion

        public PokemonDatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultDatabase"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DamageMultiplier>()
                .HasOne(dm => dm.Type)
                .WithMany()
                .HasForeignKey(dm => dm.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DamageMultiplier>()
                .HasOne(dm => dm.Against)
                .WithMany()
                .HasForeignKey(dm => dm.AgainstId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlternateForm>()
                .HasOne(af => af.Original)
                .WithMany()
                .HasForeignKey(af => af.OriginalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlternateForm>()
                .HasOne(af => af.Alternate)
                .WithMany()
                .HasForeignKey(af => af.AlternateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evolution>()
               .HasOne(ev => ev.Pokemon)
               .WithMany()
               .HasForeignKey(ev => ev.PokemonId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evolution>()
                .HasOne(ev => ev.Into)
                .WithMany()
                .HasForeignKey(ev => ev.IntoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PokemonMove>()
               .HasOne(pa => pa.Pokemon)
               .WithMany(at => at.PokemonMoves)
               .HasForeignKey(pa => pa.PokemonId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PokemonMove>()
                .HasOne(pa => pa.Move)
                .WithMany(at => at.PokemonMoves)
                .HasForeignKey(pa => pa.MoveId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
