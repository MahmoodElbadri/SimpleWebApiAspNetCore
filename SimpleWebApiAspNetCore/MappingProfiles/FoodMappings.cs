using AutoMapper;
using SimpleWebApiAspNetCore.DTOs;
using SimpleWebApiAspNetCore.Entities;

namespace SimpleWebApiAspNetCore.MappingProfiles;

public class FoodMappings:Profile
{
    public FoodMappings()
    {
        CreateMap<FoodEntity, FoodResponse>().ReverseMap();
        CreateMap<FoodEntity, FoodAddRequest>().ReverseMap();
        CreateMap<FoodEntity, FoodUpdateRequest>().ReverseMap();
    }
}
