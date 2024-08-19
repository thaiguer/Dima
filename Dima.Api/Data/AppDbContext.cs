using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dima.Api.Data;

public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext (options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //gonna search for classes that implement IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //the connection string is set in dotnet user-secrets
        //in the format of appsettings.json, that has the default connection empty
        //dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1,1433;Database=dima-dev;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;"
        //Server=127.0.0.1,1433;Database=dima-dev;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;
    }
}
