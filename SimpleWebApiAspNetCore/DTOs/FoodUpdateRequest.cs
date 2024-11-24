namespace SimpleWebApiAspNetCore.DTOs;

public class FoodUpdateRequest
{
    public string? Name { get; set; }
    public string? Type{ get; set; }
    public int Calories { get; set; }
    public DateTime? Created { get; set; }
}
