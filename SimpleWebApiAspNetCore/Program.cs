using SimpleWebApiAspNetCore.Repositories;
using SimpleWebApiAspNetCore.Services;
using Microsoft.EntityFrameworkCore;
using SimpleWebApiAspNetCore.MappingProfiles;

namespace SimpleWebApiAspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<ISeedDataService, SeedDataService>();
        builder.Services.AddScoped<IFoodRepository, FoodRepository>();
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb");
        });
        builder.Services.AddAutoMapper(typeof(FoodMappings));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}
