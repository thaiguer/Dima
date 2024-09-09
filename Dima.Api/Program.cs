using Dima.Api.Data;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Responses;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using System.Diagnostics;
using Dima.Api.Endpoints;
using Microsoft.IdentityModel.Tokens;
using Dima.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.
    Configuration.
    GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(connectionString); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", Health.GetHealthMessageApi);
app.MapGet("/db", Health.GetHealthMessageDataBase);
app.MapEndpoints();

app.Run();

