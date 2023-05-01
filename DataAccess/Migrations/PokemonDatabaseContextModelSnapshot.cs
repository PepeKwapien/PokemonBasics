﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PokemonDatabaseContext))]
    partial class PokemonDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AbilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbilityId");

                    b.HasIndex("PokemonId");

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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimalLevel")
                        .HasColumnType("int");

                    b.Property<Guid>("MoveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("MoveId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonMoves");
                });

            modelBuilder.Entity("Models.Pokeballs.Pokeball", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Pokeballs");
                });

            modelBuilder.Entity("Models.Pokedexes.Pokedex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
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

            modelBuilder.Entity("Models.Pokedexes.PokemonEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<Guid>("PokedexId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PokedexId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonEntries");
                });

            modelBuilder.Entity("Models.Pokemons.Evolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BabyItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeldItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IntoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("KnownMoveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("KnownMoveTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MinAffection")
                        .HasColumnType("int");

                    b.Property<int?>("MinBeauty")
                        .HasColumnType("int");

                    b.Property<int?>("MinHappiness")
                        .HasColumnType("int");

                    b.Property<int?>("MinLevel")
                        .HasColumnType("int");

                    b.Property<bool>("OverworldRain")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PartySpeciesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PartyTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RelativePhysicalStats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeOfDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TradeSpeciesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Trigger")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("TurnUpsideDown")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IntoId");

                    b.HasIndex("KnownMoveId");

                    b.HasIndex("KnownMoveTypeId");

                    b.HasIndex("PartySpeciesId");

                    b.HasIndex("PartyTypeId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("TradeSpeciesId");

                    b.ToTable("Evolutions");
                });

            modelBuilder.Entity("Models.Pokemons.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<bool>("Baby")
                        .HasColumnType("bit");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<string>("EggGroups")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GenerationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("Habitat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasGenderDifferences")
                        .HasColumnType("bit");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("Legendary")
                        .HasColumnType("bit");

                    b.Property<bool>("Mythical")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid>("PrimaryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SecondaryTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Shape")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasIndex("PrimaryTypeId");

                    b.HasIndex("SecondaryTypeId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("Models.Pokemons.RegionalVariant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OriginalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OriginalId");

                    b.HasIndex("VariantId");

                    b.ToTable("RegionalVariants");
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

            modelBuilder.Entity("Models.Pokeballs.Pokeball", b =>
                {
                    b.HasOne("Models.Games.Game", null)
                        .WithMany("Pokeballs")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Models.Pokedexes.PokemonEntry", b =>
                {
                    b.HasOne("Models.Pokedexes.Pokedex", "Pokedex")
                        .WithMany("Pokemons")
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

            modelBuilder.Entity("Models.Pokemons.Evolution", b =>
                {
                    b.HasOne("Models.Pokemons.Pokemon", "Into")
                        .WithMany()
                        .HasForeignKey("IntoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Moves.Move", "KnownMove")
                        .WithMany()
                        .HasForeignKey("KnownMoveId");

                    b.HasOne("Models.Types.PokemonType", "KnownMoveType")
                        .WithMany()
                        .HasForeignKey("KnownMoveTypeId");

                    b.HasOne("Models.Pokemons.Pokemon", "PartySpecies")
                        .WithMany()
                        .HasForeignKey("PartySpeciesId");

                    b.HasOne("Models.Types.PokemonType", "PartyType")
                        .WithMany()
                        .HasForeignKey("PartyTypeId");

                    b.HasOne("Models.Pokemons.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "TradeSpecies")
                        .WithMany()
                        .HasForeignKey("TradeSpeciesId");

                    b.Navigation("Into");

                    b.Navigation("KnownMove");

                    b.Navigation("KnownMoveType");

                    b.Navigation("PartySpecies");

                    b.Navigation("PartyType");

                    b.Navigation("Pokemon");

                    b.Navigation("TradeSpecies");
                });

            modelBuilder.Entity("Models.Pokemons.Pokemon", b =>
                {
                    b.HasOne("Models.Generations.Generation", "Generation")
                        .WithMany("Pokemons")
                        .HasForeignKey("GenerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("Models.Pokemons.RegionalVariant", b =>
                {
                    b.HasOne("Models.Pokemons.Pokemon", "Original")
                        .WithMany()
                        .HasForeignKey("OriginalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Models.Pokemons.Pokemon", "Variant")
                        .WithMany()
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Original");

                    b.Navigation("Variant");
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

            modelBuilder.Entity("Models.Games.Game", b =>
                {
                    b.Navigation("Pokeballs");
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
