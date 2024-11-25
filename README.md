

# Simple Web API with ASP.NET Core

This repository contains a sample ASP.NET Core Web API for managing food items. The API is designed to showcase best practices in RESTful API development, including HATEOAS, pagination, and versioning.

## Features
- **CRUD Operations**: Create, Read, Update, and Delete food items.
- **HATEOAS**: Hypermedia links provided for all resources.
- **API Versioning**: Supports multiple versions of the API.
- **Pagination**: Metadata and navigation links for paginated responses.
- **Swagger Integration**: Auto-generated API documentation.
- **In-Memory Database**: Simplifies testing and development.
- **AutoMapper**: Handles object-to-object mapping.
- **Dependency Injection**: Fully modular and extensible architecture.

## Prerequisites
- [.NET 6 or later](https://dotnet.microsoft.com/download)
- Visual Studio or VS Code

## Getting Started

### Clone the Repository

### Run the Application
1. Open the project in Visual Studio or your favorite IDE.
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to `https://localhost:5001/swagger` to explore the API using Swagger UI.

### Endpoints
- **GET /api/v1/foods**: Retrieve all food items with pagination and HATEOAS links.
- **POST /api/v1/foods**: Add a new food item.
- **GET /api/v1/foods/{id}**: Retrieve a specific food item.
- **PUT /api/v1/foods/{id}**: Update a food item.
- **DELETE /api/v1/foods/{id}**: Delete a food item.

### Example Request and Response

#### GET `/api/v1/foods?page=1&pagecount=10`
**Response:**
```json
{
  "value": [
    {
      "id": 1,
      "name": "Apple",
      "calories": 95,
      "links": [
        { "rel": "self", "href": "/api/v1/foods/1", "method": "GET" },
        { "rel": "update_food", "href": "/api/v1/foods/1", "method": "PUT" },
        { "rel": "delete", "href": "/api/v1/foods/1", "method": "DELETE" }
      ]
    }
  ],
  "links": [
    { "rel": "self", "href": "/api/v1/foods?page=1&pagecount=10", "method": "GET" },
    { "rel": "next", "href": "/api/v1/foods?page=2&pagecount=10", "method": "GET" },
    { "rel": "create", "href": "/api/v1/foods", "method": "POST" }
  ]
}
```

## Key Libraries and Tools
- **ASP.NET Core**: The foundation of the application.
- **Entity Framework Core**: In-memory database for rapid development.
- **Swagger & Swashbuckle**: API documentation and testing.
- **AutoMapper**: Object mapping for cleaner code.
- **Newtonsoft.Json**: JSON serialization and formatting.

## Customization
### Adding New Endpoints
1. Create a new controller or extend an existing one.
2. Use attributes like `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, or `[HttpDelete]` to define routes.
3. Implement the necessary logic using services and repositories.

### Database Integration
To switch from an in-memory database to a real database (e.g., SQL Server):
1. Update the `AddDbContext` call in `Program.cs`:
   ```csharp
   options.UseSqlServer("YourConnectionString");
   ```
2. Apply migrations using:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
