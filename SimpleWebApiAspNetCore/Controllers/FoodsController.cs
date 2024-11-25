using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApiAspNetCore.Entities;
using SimpleWebApiAspNetCore.Models;
using SimpleWebApiAspNetCore.Repositories;

namespace SimpleWebApiAspNetCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodsController:ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMapper _mapper;

    public FoodsController(IFoodRepository foodRepository, IMapper mapper)
    {
        _foodRepository = foodRepository;
        _mapper = mapper;
    }

    public IActionResult GetAll()
    {
        return Ok();
    }
    
}
