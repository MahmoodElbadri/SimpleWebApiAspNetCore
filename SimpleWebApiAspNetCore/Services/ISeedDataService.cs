using SimpleWebApiAspNetCore.Repositories;

namespace SimpleWebApiAspNetCore.Services;

public interface ISeedDataService
{
    void Initialize(ApplicationDbContext context);
}
