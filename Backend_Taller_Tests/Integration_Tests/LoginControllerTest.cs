using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller_Tests.Integration_Tests
{
    [Collection("Database collection")]
    public class LoginControllerTest : IClassFixture<WebAppFactory>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly WebAppFactory _factory;
        public LoginControllerTest(WebAppFactory factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        public async Task DisposeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task LoginController_Login_ReturnsOk()
        {
            // Arrange
            await SeedDbAsync();

            var loginData = new
            {
                Email = "Jonathan@gmail.com",
                Password = "123456789"
            };
            // Act
            var response = await _client.PostAsJsonAsync("/api/Login", loginData);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.TryGetValues("Set-Cookie", out var cookie));
            Assert.Contains(cookie, c => c.StartsWith("jwt"));
        }

        private async Task SeedDbAsync()
        {
            using (var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<TallerDbContext>())
            {
                await dbContext.Roles.AddAsync(new Roles() { RolesId = Guid.NewGuid(), Name = "USER" });
                await dbContext.SaveChangesAsync();

                var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "USER");

                await dbContext.Users.AddAsync(new Users() { Email = "Jonathan@gmail.com", Password = "$2a$11$U6dLcy4EVKXINWT48gSs3edx2HzTLCMHfJBN4wVyS47co7C7ChpUi", Role = role });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
