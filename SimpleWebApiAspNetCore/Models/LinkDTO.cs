namespace SimpleWebApiAspNetCore.Models;

public class LinkDTO
{
    public string? Href { get; set; }
    public string? Rel { get; set; }
    public string? Method { get; set; }

    public LinkDTO(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}
