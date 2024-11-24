using SimpleWebApiAspNetCore.Models;

namespace SimpleWebApiAspNetCore.Helpers;

public static class QueryParameterExtensions
{
    public static bool HasPrevious(this QueryParameters queryParameters)
    {
        return (queryParameters.Page > 1);
    }
    public static bool IsDescending(this QueryParameters queryParameters)
    {
        if (!string.IsNullOrEmpty(queryParameters.OrderBy))
        {
            return queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
        }

        return false;
    }
}
