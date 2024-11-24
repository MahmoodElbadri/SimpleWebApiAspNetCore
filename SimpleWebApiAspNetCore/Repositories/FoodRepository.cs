using System.Linq.Dynamic.Core;
using SimpleWebApiAspNetCore.Entities;
using SimpleWebApiAspNetCore.Helpers;
using SimpleWebApiAspNetCore.Models;

namespace SimpleWebApiAspNetCore.Repositories;

public class FoodRepository:IFoodRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FoodRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public FoodEntity? GetSingle(int id)
    {
        return _dbContext.FoodEntities.FirstOrDefault(tmp => tmp.Id == id);
    }

    public void Add(FoodEntity item)
    {
        _dbContext.FoodEntities.Add(item);
    }

    public void Delete(int id)
    {
        FoodEntity? foodItem = GetSingle(id);
        if (foodItem != null)
        {
            _dbContext.FoodEntities.Remove(foodItem);
        }
    }

    public FoodEntity Update(int id, FoodEntity item)
    {
        _dbContext.FoodEntities.Update(item);
        return item;
    }

    public List<FoodEntity> GetAll(QueryParameters queryParameters)
    {
        // Apply sorting dynamically
        string orderByClause = queryParameters.IsDescending()
            ? $"{queryParameters.OrderBy} descending"
            : queryParameters.OrderBy;

        // Use IQueryable to perform database-side operations
        IQueryable<FoodEntity> foodList = _dbContext.FoodEntities
            .OrderBy(orderByClause) // Dynamic sorting using System.Linq.Dynamic.Core
            .Skip(queryParameters.PageCount * (queryParameters.Page - 1)) // Skip records for pagination
            .Take(queryParameters.PageCount); // Take only the required records

        // Execute the query and return the result
        return foodList.ToList();
    }


    public ICollection<FoodEntity> GetRandomMeal()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        throw new NotImplementedException();
    }
}
