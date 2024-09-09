﻿using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithDescription("Edit a transaction that has already been created. The Id cannot be changed")
            .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        UpdateTransactionRequest request,
        long id
        )
    {
        request.UserId = "temp dev user";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}