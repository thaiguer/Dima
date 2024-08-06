var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "caraca!");
app.MapPost(
    "/v1/transactions",
    () => new Response
    {
        Id = 2,
        Title = "alguma coisa"
    })
    .Produces<Response>();

app.Run();

public class Request()
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Type { get; set; }
    public decimal Ammount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

public class Response()
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
