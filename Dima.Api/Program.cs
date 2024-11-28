using Dima.Api.Endpoints;
using Dima.Api.Common.Api;
using Dima.Api;
using Dima.Core;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.ConfigureDevelopmentEnviroment();
}

app.Urls.Add(Configuration.BackendUrl);
app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();
app.Run();