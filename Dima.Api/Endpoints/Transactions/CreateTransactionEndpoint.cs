using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Responses;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithDescription("Create a new transaction")
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}