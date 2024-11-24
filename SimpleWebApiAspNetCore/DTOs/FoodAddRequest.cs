using System.ComponentModel.DataAnnotations;

namespace SimpleWebApiAspNetCore.DTOs;

public class FoodAddRequest
{
    [Required]
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int  Calories { get; set; }
    public DateTime  Created { get; set; }
}
