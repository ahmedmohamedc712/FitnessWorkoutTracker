using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace PublicApiIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MsSqlContainer _dbContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2025-latest")
        .Build();

    private Respawner _respawner = default!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection",
            _dbContainer.GetConnectionString()); // Make sure to use the same key of connection string used in development
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        await using var connection = new SqlConnection(_dbContainer.GetConnectionString());
        await connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"],
            DbAdapter = DbAdapter.SqlServer
        });
    }
    public async Task ResetDatabaseAsync()
    {
        await using var connection = new SqlConnection(_dbContainer.GetConnectionString());
        await connection.OpenAsync();

        await _respawner.ResetAsync(connection);
    }
    
    public async Task SeedAsync(Func<AppDbContext, Task> action)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await action(dbContext);
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
