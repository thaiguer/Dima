using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Dima.Core;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Get Transactions by period")
            .WithDescription("Get a list of Transactions from the user")
            .Produces<PagedResponse<List<Transaction>>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        DateTime? start = null,
        DateTime? finish = null,
        int pageNumber = Configuration.DefaultPageNumber,
        int pageSize = Configuration.DefaultPageSize
        )
    {
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = "temp dev user",
            PageNumber = pageNumber,
            PageSize = pageSize,
            Start = start,
            Finish = finish
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}