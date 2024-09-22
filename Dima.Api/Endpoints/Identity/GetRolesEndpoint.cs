﻿using System.Security.Claims;
using Dima.Api.Common.Api;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles", Handle).RequireAuthorization();
    }

    private static Task<IResult> Handle(ClaimsPrincipal user)
    {
        //do with UserManager<User> if necessary check from DB
        //the ClaimsPrincipal gets Roles from the cookie

        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return Task.FromResult(Results.Unauthorized());
        }

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}