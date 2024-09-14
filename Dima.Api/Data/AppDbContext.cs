using Dima.Api.Models;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dima.Api.Data;

public class AppDbContext (DbContextOptions<AppDbContext> options)
    : IdentityDbContext
    <
        User,
        IdentityRole<long>,
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
    >
    (options)
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

        //dotnet ef migrations add V1_correct_typo
        //dotnet ef database update

        //dotnet new gitignore
        //dotnet new sln
        //dotnet sln add project
    }
}