using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SimpleWebApiAspNetCore.Helpers;
using SimpleWebApiAspNetCore.Models;

namespace SimpleWebApiAspNetCore.Services;

public class LinkService<T> : ILinkService<T>
{
    private readonly IUrlHelper _urlHelper;

    public LinkService(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        // Ensure actionContextAccessor.ActionContext is not null
        if (actionContextAccessor.ActionContext is null)
        {
            throw new ArgumentNullException(nameof(actionContextAccessor.ActionContext), "ActionContext cannot be null.");
        }

        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext)
            ?? throw new InvalidOperationException("Failed to create IUrlHelper.");
    }

    public List<LinkDTO> CreateLinksForCollection(QueryParameters queryParameters, int totalCount, ApiVersion version)
    {
        Type controllerType = typeof(T);
        MethodInfo[] methods = controllerType.GetMethods();

        var links = new List<LinkDTO>();
        var getAllMethodName = GetMethod(methods, typeof(HttpGetAttribute), 0);

        if (!string.IsNullOrEmpty(getAllMethodName))
        {
            links.Add(new LinkDTO(_urlHelper.Link(getAllMethodName, new
            {
                pagecount = queryParameters.PageCount,
                page = queryParameters.Page,
                orderby = queryParameters.OrderBy
            }) ?? string.Empty, "self", "GET"));

            links.Add(new LinkDTO(_urlHelper.Link(getAllMethodName, new
            {
                pagecount = queryParameters.PageCount,
                page = 1,
                orderby = queryParameters.OrderBy
            }) ?? string.Empty, "first", "GET"));

            links.Add(new LinkDTO(_urlHelper.Link(getAllMethodName, new
            {
                pagecount = queryParameters.PageCount,
                page = queryParameters.GetTotalPages(totalCount),
                orderby = queryParameters.OrderBy
            }) ?? string.Empty, "last", "GET"));

            if (queryParameters.HasNext(totalCount))
            {
                links.Add(new LinkDTO(_urlHelper.Link(getAllMethodName, new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page + 1,
                    orderby = queryParameters.OrderBy
                }) ?? string.Empty, "next", "GET"));
            }

            if (queryParameters.HasPrevious())
            {
                links.Add(new LinkDTO(_urlHelper.Link(getAllMethodName, new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page - 1,
                    orderby = queryParameters.OrderBy
                }) ?? string.Empty, "previous", "GET"));
            }
        }

        var postUrl = _urlHelper.Link(GetMethod(methods, typeof(HttpPostAttribute)) ?? string.Empty, new { version = version.ToString() });
        links.Add(new LinkDTO(postUrl ?? string.Empty, "create", "POST"));

        return links;
    }

    private string? GetMethod(MethodInfo[] methods, Type type, int routeParamsLength = 0)
    {
        var filteredMethods = methods.Where(m => m.GetCustomAttributes(type, false).Any()).ToArray();

        if (filteredMethods.Length == 0)
        {
            return null;
        }

        if (routeParamsLength == 0)
        {
            return filteredMethods.FirstOrDefault()?.Name;
        }

        foreach (var method in filteredMethods)
        {
            var routeAttribs = method.GetCustomAttributes(typeof(RouteAttribute));

            if (routeAttribs.Count() == routeParamsLength)
            {
                return method.Name;
            }
        }

        return null;
    }

    public object ExpandSingleFoodItem(object resource, int identifier, ApiVersion version)
    {
        if (resource is null) throw new ArgumentNullException(nameof(resource), "Resource cannot be null.");

        var resourceToReturn = resource.ToDynamic() as IDictionary<string, object> 
            ?? throw new InvalidOperationException("Failed to convert resource to dynamic.");

        var links = GetLinksForSingleItem(identifier, version);
        resourceToReturn.Add("links", links);

        return resourceToReturn;
    }

    private IEnumerable<LinkDTO> GetLinksForSingleItem(int id, ApiVersion version)
    {
        Type myType = typeof(T);
        MethodInfo[] methods = myType.GetMethods();
        var links = new List<LinkDTO>();

        var getLink = _urlHelper.Link(GetMethod(methods, typeof(HttpGetAttribute), 1) ?? string.Empty, new { version = version.ToString(), id });
        if (!string.IsNullOrEmpty(getLink))
        {
            links.Add(new LinkDTO(getLink, "self", "GET"));
        }

        var deleteLink = _urlHelper.Link(GetMethod(methods, typeof(HttpDeleteAttribute)) ?? string.Empty, new { version = version.ToString(), id });
        if (!string.IsNullOrEmpty(deleteLink))
        {
            links.Add(new LinkDTO(deleteLink, "delete", "DELETE"));
        }

        var createLink = _urlHelper.Link(GetMethod(methods, typeof(HttpPostAttribute)) ?? string.Empty, new { version = version.ToString() });
        if (!string.IsNullOrEmpty(createLink))
        {
            links.Add(new LinkDTO(createLink, "create_food", "POST"));
        }

        var updateLink = _urlHelper.Link(GetMethod(methods, typeof(HttpPutAttribute)) ?? string.Empty, new { version = version.ToString(), id });
        if (!string.IsNullOrEmpty(updateLink))
        {
            links.Add(new LinkDTO(updateLink, "update_food", "PUT"));
        }

        return links;
    }
}
