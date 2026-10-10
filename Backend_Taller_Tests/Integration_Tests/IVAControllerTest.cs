
using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace Backend_Taller_Tests.Integration_Tests
{
    [Collection("Database collection")]
    public class IVAControllerTest : IClassFixture<WebAppFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly WebAppFactory _factory;
        public IVAControllerTest(WebAppFactory factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task ObtenerIVA_ReturnsOk()
        {
            //Arrange
            await SeedDbAsync();
            // Act
            var response = await _client.GetAsync("/api/IVA");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0.16m.ToString(), await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task ModificarIVA_ReturnsOk()
        {
            //Arrange
            await SeedDbAsync();
            var IvaData = new { IvaValue = 0.18m };

            // Act
            var response = await _client.PutAsJsonAsync("/api/IVA", IvaData);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0.18m.ToString(), await response.Content.ReadAsStringAsync());
        }

        public async Task DisposeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        private async Task SeedDbAsync()
        {
            using var _context =_factory.Services.CreateScope().ServiceProvider.GetRequiredService<TallerDbContext>();
            await _context.Iva.AddAsync(new Iva() { IvaValue = 0.16m});
            await _context.SaveChangesAsync();

        }

    }
}
