﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PokemonDatabaseContext))]
    [Migration("20230422135053_games_fix")]
    partial class gamesfix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamePokeball", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokeballsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PokeballsId");

                    b.HasIndex("PokeballsId");

                    b.ToTable("GamePokeball");
                });

            modelBuilder.Entity("GamePokedex", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokedexesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PokedexesId");

                    b.HasIndex("PokedexesId");

                    b.ToTable("GamePokedex");
                });

            modelBuilder.Entity("GenerationPokeball", b =>
                {
                    b.Property<Guid>("GenerationsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokeballsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GenerationsId", "PokeballsId");

                    b.HasIndex("PokeballsId");

                    b.ToTable("GenerationPokeball");
                });

            modelBuilder.Entity("Models.Abilities.Ability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Effect")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("OverworldEffect")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("Models.Abilities.PokemonAbility", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AbilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.HasKey("PokemonId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.ToTable("PokemonAbilities");
                });

            modelBuilder.Entity("Models.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MainRegion")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Models.Generations.Generation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Generations");
                });

            modelBuilder.Entity("Models.Moves.Move", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Effect")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("PP")
                        .HasColumnType("int");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int?>("SpecialEffectChance")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.HasIndex("TypeId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Models.Moves.PokemonMove", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MoveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimalLevel")
                        .HasColumnType("int");

                    b.HasKey("PokemonId", "MoveId");

                    b.HasIndex("GameId");

                    b.HasIndex("MoveId");

                    b.ToTable("PokemonMoves");
                });

            modelBuilder.Entity("Models.Pokeballs.Pokeball", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Pokeballs");
                });

            modelBuilder.Entity("Models.Pokedexes.Pokedex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Region")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Pokedexes");
                });

            modelBuilder.Entity("Models.Pokedexes.PokemonAvailability", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokedexId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("PokemonId", "PokedexId");

                    b.HasIndex("PokedexId");

                    b.ToTable("PokemonAvailabilities");
                });

            modelBuilder.Entity("Models.Pokemons.AlternateForm", b =>
                {
                    b.Property<Guid>("OriginalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlternateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OriginalId", "AlternateId");

                    b.HasIndex("AlternateId");

                    b.ToTable("AlternateForms");
                });

            modelBuilder.Entity("Models.Pokemons.Evolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("IntoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IntoId");

                    b.HasIndex("PokemonId");

                    b.ToTable("Evolutions");
                });

            modelBuilder.Entity("Models.Pokemons.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("DexNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Image")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid?>("PokedexId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PrimaryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SecondaryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SpecialAttack")
                        .HasColumnType("int");

                    b.Property<int>("SpecialDefense")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GenerationId");

                    b.HasIndex("PokedexId");

                    b.HasIndex("PrimaryTypeId");

                    b.HasIndex("SecondaryTypeId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("Models.Types.DamageMultiplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgainstId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Multiplier")
                        .HasColumnType("float");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AgainstId");

                    b.HasIndex("TypeId");

                    b.ToTable("DamageMultipliers");
                });

            modelBuilder.Entity("Models.Types.PokemonType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("GamePokeball", b =>
                {
                    b.HasOne("Models.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokeballs.Pokeball", null)
                        .WithMany()
                        .HasForeignKey("PokeballsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GamePokedex", b =>
                {
                    b.HasOne("Models.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokedexes.Pokedex", null)
                        .WithMany()
                        .HasForeignKey("PokedexesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenerationPokeball", b =>
                {
                    b.HasOne("Models.Generations.Generation", null)
                        .WithMany()
                        .HasForeignKey("GenerationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokeballs.Pokeball", null)
                        .WithMany()
                        .HasForeignKey("PokeballsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Abilities.Ability", b =>
                {
                    b.HasOne("Models.Generations.Generation", "Generation")
                        .WithMany("Abilities")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generation");
                });

            modelBuilder.Entity("Models.Abilities.PokemonAbility", b =>
                {
                    b.HasOne("Models.Abilities.Ability", "Ability")
                        .WithMany("PokemonAbilities")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Pokemon")
                        .WithMany("PokemonAbilities")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Models.Games.Game", b =>
                {
                    b.HasOne("Models.Generations.Generation", "Generation")
                        .WithMany("Games")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generation");
                });

            modelBuilder.Entity("Models.Moves.Move", b =>
                {
                    b.HasOne("Models.Generations.Generation", "Generation")
                        .WithMany("Moves")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Types.PokemonType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generation");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Models.Moves.PokemonMove", b =>
                {
                    b.HasOne("Models.Games.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Moves.Move", "Move")
                        .WithMany("PokemonMoves")
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Pokemon")
                        .WithMany("PokemonMoves")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Move");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Models.Pokedexes.PokemonAvailability", b =>
                {
                    b.HasOne("Models.Pokedexes.Pokedex", "Pokedex")
                        .WithMany()
                        .HasForeignKey("PokedexId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Pokemon")
                        .WithMany("PokemonAvailabilities")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokedex");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Models.Pokemons.AlternateForm", b =>
                {
                    b.HasOne("Models.Pokemons.Pokemon", "Alternate")
                        .WithMany()
                        .HasForeignKey("AlternateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Original")
                        .WithMany()
                        .HasForeignKey("OriginalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Alternate");

                    b.Navigation("Original");
                });

            modelBuilder.Entity("Models.Pokemons.Evolution", b =>
                {
                    b.HasOne("Models.Pokemons.Pokemon", "Into")
                        .WithMany()
                        .HasForeignKey("IntoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Into");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Models.Pokemons.Pokemon", b =>
                {
                    b.HasOne("Models.Generations.Generation", "Generation")
                        .WithMany("Pokemons")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Pokedexes.Pokedex", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("PokedexId");

                    b.HasOne("Models.Types.PokemonType", "PrimaryType")
                        .WithMany()
                        .HasForeignKey("PrimaryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Types.PokemonType", "SecondaryType")
                        .WithMany()
                        .HasForeignKey("SecondaryTypeId");

                    b.Navigation("Generation");

                    b.Navigation("PrimaryType");

                    b.Navigation("SecondaryType");
                });

            modelBuilder.Entity("Models.Types.DamageMultiplier", b =>
                {
                    b.HasOne("Models.Types.PokemonType", "Against")
                        .WithMany()
                        .HasForeignKey("AgainstId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Types.PokemonType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Against");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Models.Abilities.Ability", b =>
                {
                    b.Navigation("PokemonAbilities");
                });

            modelBuilder.Entity("Models.Generations.Generation", b =>
                {
                    b.Navigation("Abilities");

                    b.Navigation("Games");

                    b.Navigation("Moves");

                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("Models.Moves.Move", b =>
                {
                    b.Navigation("PokemonMoves");
                });

            modelBuilder.Entity("Models.Pokedexes.Pokedex", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("Models.Pokemons.Pokemon", b =>
                {
                    b.Navigation("PokemonAbilities");

                    b.Navigation("PokemonAvailabilities");

                    b.Navigation("PokemonMoves");
                });
#pragma warning restore 612, 618
        }
    }
}