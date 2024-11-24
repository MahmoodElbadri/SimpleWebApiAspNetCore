using SimpleWebApiAspNetCore.Entities;
using SimpleWebApiAspNetCore.Models;

namespace SimpleWebApiAspNetCore.Repositories;

public interface IFoodRepository
{
    FoodEntity? GetSingle(int id);
    void Add(FoodEntity item);
    void Delete(int id);
    FoodEntity Update(int id, FoodEntity item);
    List<FoodEntity> GetAll(QueryParameters queryParameters);
    ICollection<FoodEntity> GetRandomMeal();
    int Count();
    bool Save();
}
