using SimpleWebApiAspNetCore.Entities;
using SimpleWebApiAspNetCore.Repositories;

namespace SimpleWebApiAspNetCore.Services;

public class SeedDataService:ISeedDataService
{
    public void Initialize(ApplicationDbContext context)
    {
        List<FoodEntity> foodsList = new List<FoodEntity>()
        {
            new FoodEntity() { Calories = 1000, Type = "Starter", Name = "Lasagne", Created = DateTime.Now },
            new FoodEntity() { Calories = 1100, Type = "Main", Name = "Hamburger", Created = DateTime.Now },
            new FoodEntity() { Calories = 1200, Type = "Dessert", Name = "Spaghetti", Created = DateTime.Now },
            new FoodEntity() { Calories = 1300, Type = "Starter", Name = "Pizza", Created = DateTime.Now },
            new FoodEntity() { Calories = 1400, Type = "Main", Name = "Steak", Created = DateTime.Now },
            new FoodEntity() { Calories = 1500, Type = "Dessert", Name = "Ice Cream", Created = DateTime.Now }
        };
        context.AddRange(foodsList);
        context.SaveChanges();
    }
}
