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

    public static bool HasNext(this QueryParameters queryParameters, int totalCount)
    {
        return (queryParameters.Page < (int)GetTotalPages(queryParameters, totalCount));
    }

    public static double GetTotalPages(this QueryParameters queryParameters, int totalCount)
    {
        return Math.Ceiling(totalCount / (double)queryParameters.PageCount);
    }

    public static bool HasQuery(this QueryParameters queryParameters)
    {
        return !String.IsNullOrEmpty(queryParameters.Query);
    }
}
