using Microsoft.EntityFrameworkCore;
using SimpleWebApiAspNetCore.Entities;

namespace SimpleWebApiAspNetCore.Repositories;

public class ApplicationDbContext:DbContext
{
    public DbSet<FoodEntity> FoodEntities { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
