using DataAccess;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Repositories;
using PokemonAPI.Services;

namespace PokemonAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<IPokemonDatabaseContext, PokemonDatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

            // Repositories
            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<IPokemonTypeRepository, PokemonTypeRepository>();
            builder.Services.AddScoped<IAbilityRepository, AbilityRepository>();

            // Services
            builder.Services.AddScoped<IPokemonService, PokemonService>();
            builder.Services.AddScoped<IPokemonTypeService, PokemonTypeService>();
            builder.Services.AddScoped<IAbilityService, AbilityService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string pokeWeaknessCorsName = builder.Configuration.GetValue<string>("Cors:PokeWeakness:Name");
            string pokeWeaknessCorsUrl = builder.Configuration.GetValue<string>("Cors:PokeWeakness:Url");

            builder.Services.AddCors(options => options.AddPolicy(pokeWeaknessCorsName,
                builder => builder
                .WithOrigins(pokeWeaknessCorsUrl)
                .AllowAnyMethod()
                .AllowAnyHeader()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
            {
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(pokeWeaknessCorsName);
            app.UseAuthorization();

            app.MapControllers();
            
            app.Run();
        }
    }
}