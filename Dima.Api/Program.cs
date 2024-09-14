using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dima.Api.Endpoints;
using Dima.Api.Common.Api;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", Health.GetHealthMessageApi);
app.MapGet("/db", Health.GetHealthMessageDataBase);
app.MapEndpoints();

app.Run();