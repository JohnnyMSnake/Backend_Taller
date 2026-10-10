
using Backend_Taller.EFConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Testcontainers.MsSql;

namespace Backend_Taller_Tests.Integration_Tests
{
    public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {

        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
            .WithDatabase("Base_Taller")
            .WithPassword("aPassword_123456789!")
            .Build();

        private Respawner _respawner = null!;


        public async Task ResetDatabaseAsync()
        {
            using var connection = new SqlConnection(_msSqlContainer.GetConnectionString());
            await connection.OpenAsync();
            await _respawner.ResetAsync(connection);
        }
        public async Task InitializeAsync()
        {
            await _msSqlContainer.StartAsync();



            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TallerDbContext>();

                await db.Database.MigrateAsync();
            }

            using var connection = new SqlConnection(_msSqlContainer.GetConnectionString());
            await connection.OpenAsync();

            _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                SchemasToInclude = new[] { "dbo" },
                TablesToIgnore = new Table[] { new Table("__EFMigrationsHistory") }

            });


        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.FirstOrDefault(service => service.ServiceType == typeof(DbContextOptions<TallerDbContext>));

                if (dbContext is not null)
                {
                    services.Remove(dbContext);
                }

                services.AddDbContext<TallerDbContext>(options =>
                {
                    options.UseSqlServer(_msSqlContainer.GetConnectionString());
                });

            });
        }

        public async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
            await _msSqlContainer.DisposeAsync();
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<WebAppFactory>
    {

    }

}
