using DataAccess;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Repositories;

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

            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options => options.AddPolicy("AllowLocalhost4200",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost4200");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}