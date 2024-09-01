using Dima.Api.Data;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.
    Configuration.
    GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(connectionString); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var runningSince = Process.GetCurrentProcess().StartTime.ToUniversalTime();
app.MapGet("/", () => $"It's alive. The API is running since {runningSince} (UTC).");

app.MapPost(
    "/v1/categories",
    (CreateCategoryRequest request, ICategoryHandler handler) => handler.CreateAsync(request))
    .WithName("Categories: Create").WithSummary("Cria uma nova categoria").Produces<Response<Category?>>();

app.MapGet(
    "/v1/categories",
    async (ICategoryHandler handler) =>
    {
        GetAllCategoriesRequest request = new GetAllCategoriesRequest();
        request.UserId = "string";
        return await handler.GetAllAsync(request);
    })
    .WithName("Categories: Read all").WithSummary("Ler categorias").Produces<PagedResponse<List<Category>>>();

app.MapGet(
    "/v1/categories/{id}",
    async (long id, ICategoryHandler handler) =>
    {
        var request = new GetCategoryByIdRequest
        {
            Id = id
        };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categories: Read by Id").WithSummary("Ler uma categoria").Produces<Response<Category?>>();

app.MapPut(
    "/v1/categories/{id}",
    async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update").WithSummary("Editar uma categoria").Produces<Response<Category?>>();

app.MapDelete(
    "/v1/categories/{id}",
    async (long id, ICategoryHandler handler) =>
    {
        var request = new DeleteCategoryRequest
        {
            Id = id
        };
        return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete").WithSummary("Deletar uma categoria").Produces<Response<Category?>>();

app.Run();