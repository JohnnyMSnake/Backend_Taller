
using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace Backend_Taller_Tests.Integration_Tests
{
    [Collection("Database collection")]
    public class MarcasControllerTest : IClassFixture<WebAppFactory>, IAsyncLifetime
    {
        private readonly WebAppFactory _factory;
        private readonly HttpClient _client;

        public MarcasControllerTest(WebAppFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        [Fact]
        public async Task ObtenerMarcas_ReturnsOkResponse()
        {
            // Arrange
            await SeedDbAsync();

            // Act
            var response = await _client.GetAsync("/api/Marcas");
            // Assert
            response.EnsureSuccessStatusCode();
            var marcas = await response.Content.ReadFromJsonAsync<List<Marcas>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(marcas);
            Assert.Collection(marcas,
                item => Assert.Equal("BMW", item.NombreMarca),
                item => Assert.Equal("Tesla", item.NombreMarca),
                item => Assert.Equal("Hyundai", item.NombreMarca),
                item => Assert.Equal("Toyota", item.NombreMarca),
                item => Assert.Equal("Honda", item.NombreMarca),
                item => Assert.Equal("Ford", item.NombreMarca),
                item => Assert.Equal("Chevrolet", item.NombreMarca),
                item => Assert.Equal("Nissan", item.NombreMarca),
                item => Assert.Equal("Kia", item.NombreMarca),
                item => Assert.Equal("Mazda", item.NombreMarca)
            );
        }

        [Fact]
        public async Task CrearMarca_ReturnsOkResponse()
        {
            // Arrange
            var nuevaMarca = new Marcas { NombreMarca = "Audi" };
            // Act
            var response = await _client.PostAsJsonAsync("/api/Marcas", nuevaMarca);
            // Assert
            response.EnsureSuccessStatusCode();
            var createdMarca = await response.Content.ReadFromJsonAsync<Marcas>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(createdMarca);
            Assert.Equal("Audi", createdMarca.NombreMarca);
        }

        private async Task SeedDbAsync()
        {
            using var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<TallerDbContext>();

            var marcas = new List<Marcas>
            {
                new Marcas { NombreMarca = "BMW" },
                new Marcas { NombreMarca = "Tesla" },
                new Marcas { NombreMarca = "Hyundai" },
                new Marcas { NombreMarca = "Toyota" },
                new Marcas { NombreMarca = "Honda" },
                new Marcas { NombreMarca = "Ford" },
                new Marcas { NombreMarca = "Chevrolet" },
                new Marcas { NombreMarca = "Nissan" },
                new Marcas { NombreMarca = "Kia" },
                new Marcas { NombreMarca = "Mazda" }

            };

            await dbContext.Marcas.AddRangeAsync(marcas);

            await dbContext.SaveChangesAsync();

        }
    }
}
