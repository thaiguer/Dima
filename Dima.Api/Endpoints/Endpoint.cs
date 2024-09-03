using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            //.RequireAuthorization()
            .MapEndPoint<CreateCategoryEndpoint>()
            .MapEndPoint<UpdateCategoryEndpoint>()
            .MapEndPoint<DeleteCategoryEndpoint>()
            .MapEndPoint<GetCategoryByIdEndpoint>()
            .MapEndPoint<GetAllCategoriesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndPoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}