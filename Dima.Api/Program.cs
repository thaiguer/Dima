using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dima.Api.Endpoints;
using Dima.Api.Common.Api;
using Microsoft.AspNetCore.Identity;
using Dima.Api.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

var connectionString = builder.
    Configuration.
    GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(connectionString); });
builder.Services
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", Health.GetHealthMessageApi);
app.MapGet("/db", Health.GetHealthMessageDataBase);
app.MapEndpoints();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/logout",
        async (SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        })
    .RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/roles",
        //do with UserManager<User> if necessary check from DB
        //the following gets Roles from the cookies
        (ClaimsPrincipal user) =>
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
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

            return TypedResults.Json(roles);
        })
    .RequireAuthorization();

app.Run();