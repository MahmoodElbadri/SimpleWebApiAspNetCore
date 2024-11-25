using Microsoft.AspNetCore.Mvc;
using SimpleWebApiAspNetCore.Models;

namespace SimpleWebApiAspNetCore.Services;

public interface ILinkService<T>
{
    object ExpandSingleFoodItem(object resource, int identifier,ApiVersion version);

    List<LinkDTO> CreateLinksForCollection(QueryParameters queryParameters, int totalCount,ApiVersion version);
}
