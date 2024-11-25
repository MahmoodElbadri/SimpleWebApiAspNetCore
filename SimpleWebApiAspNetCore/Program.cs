using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SimpleWebApiAspNetCore.Repositories;
using SimpleWebApiAspNetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SampleWebApiAspNetCore;
using SimpleWebApiAspNetCore;
using SimpleWebApiAspNetCore.Helpers;
using SimpleWebApiAspNetCore.MappingProfiles;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleWebApiAspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddSingleton<ISeedDataService, SeedDataService>();
        builder.Services.AddScoped<IFoodRepository, FoodRepository>();
        builder.Services.AddScoped(typeof(ILinkService<>), typeof(LinkService<>));
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCustomCors("AllowAll");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb");
        });
        builder.Services.AddAutoMapper(typeof(FoodMappings));
        builder.Services.AddApiVersioning();
        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        var app = builder.Build();

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToLowerInvariant()
                    );
                }
            });
            app.SeedData();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}
