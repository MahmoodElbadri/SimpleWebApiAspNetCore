using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApiAspNetCore.DTOs;
using SimpleWebApiAspNetCore.Entities;
using SimpleWebApiAspNetCore.Helpers;
using SimpleWebApiAspNetCore.Models;
using System.Text.Json;
using SimpleWebApiAspNetCore.Repositories;
using SimpleWebApiAspNetCore.Services;

namespace SimpleWebApiAspNetCore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class FoodsController:ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMapper _mapper;
    private readonly ILinkService<FoodsController> _linkService;

    public FoodsController(IFoodRepository foodRepository, IMapper mapper, ILinkService<FoodsController> linkService)
    {
        _foodRepository = foodRepository;
        _mapper = mapper;
        _linkService = linkService;
    }

    [HttpGet]
    public IActionResult GetAll(ApiVersion version, [FromQuery] QueryParameters queryParameters)
    {
        // Your code to get all items
        List<FoodEntity> foodList = _foodRepository.GetAll(queryParameters).ToList();
        var allItemsCount = foodList.Count;
        var paginationMetaData = new
        {
            totalCount = allItemsCount,
            pageSize = queryParameters.PageCount,
            currentPage = queryParameters.Page,
            hasNext = queryParameters.HasNext(allItemsCount),
            totalPages = queryParameters.GetTotalPages(allItemsCount),
            hasPrevious = queryParameters.HasPrevious(),
        };
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
        var links = _linkService.CreateLinksForCollection(queryParameters, allItemsCount, version);
        var toReturn = foodList.Select(x => _linkService.ExpandSingleFoodItem(x, x.Id, version));

        return Ok(new
        {
            value = toReturn,
            links = links
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        // Your code to get an item by ID
        return Ok();
    }

    [HttpPost]
    public IActionResult Create([FromBody] FoodAddRequest food)
    {
        // Your code to create a new item
        //return CreatedAtAction(nameof(GetById), new { id = food.Id }, food);
        return NotFound();
    }
    
}
