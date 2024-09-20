using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Identity;
using Dima.Api.Endpoints.Transactions;
using Dima.Api.Models;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGet("", Health.GetHealthMessageApi).WithTags("HealthCheck");
        endpoints.MapGet("/db", Health.GetHealthMessageDataBase).WithTags("HealthCheck");

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();
        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndPoint<LogoutEndpoint>()
            .MapEndPoint<GetRolesEndpoint>();
            
        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .MapEndPoint<CreateCategoryEndpoint>()
            .MapEndPoint<UpdateCategoryEndpoint>()
            .MapEndPoint<DeleteCategoryEndpoint>()
            .MapEndPoint<GetCategoryByIdEndpoint>()
            .MapEndPoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .MapEndPoint<CreateTransactionEndpoint>()
            .MapEndPoint<UpdateTransactionEndpoint>()
            .MapEndPoint<DeleteTransactionEndpoint>()
            .MapEndPoint<GetTransactionByIdEndpoint>()
            .MapEndPoint<GetTransactionsByPeriodEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndPoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}