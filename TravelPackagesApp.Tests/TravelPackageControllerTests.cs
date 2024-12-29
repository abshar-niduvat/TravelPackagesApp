using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelPackagesApp.Models;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.IO;

namespace TravelPackagesApp.Tests
{
    public class TravelPackageControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TravelPackageControllerTests(WebApplicationFactory<Program> factory)
        {
            
            _factory = factory.WithWebHostBuilder(builder =>
            {
                var solutionDir = Path.GetFullPath("../../../../", AppContext.BaseDirectory);
                builder.UseContentRoot(solutionDir);
            });
            
         }

        // Test for retrieving all travel packages
        [Fact]
        public async Task GetAll_ReturnsOkStatus()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/TravelPackage");

            response.EnsureSuccessStatusCode();
        }

        // Test for creating a new travel package
        [Fact]
        public async Task Create_AddsTravelPackage_ReturnsCreatedAtAction()
        {
            var client = _factory.CreateClient();
            var newPackage = new TravelPackage
            {
                Name = "Package C",
                Amount = 1000,
                IsActive = true
            };

            var response = await client.PostAsJsonAsync("/api/TravelPackage", newPackage);

            response.EnsureSuccessStatusCode();
            var createdPackage = await response.Content.ReadAsAsync<TravelPackage>();
            Assert.NotNull(createdPackage);
            Assert.Equal(newPackage.Name, createdPackage.Name);
        }

        // Test for updating an existing travel package
        [Fact]
        public async Task Update_TravelPackage_ReturnsNoContent()
        {
            var client = _factory.CreateClient();
            var existingPackage = new TravelPackage
            {
                Id = 1,
                Name = "Package A",
                Amount = 500,
                IsActive = true
            };

            existingPackage.Amount = 600;  // Modify amount
            var response = await client.PutAsJsonAsync($"/api/TravelPackage/{existingPackage.Id}", existingPackage);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        // Test for deleting a travel package
        [Fact]
        public async Task Delete_RemovesTravelPackage_ReturnsNoContent()
        {
            var client = _factory.CreateClient();
            var packageToDelete = new TravelPackage
            {
                Id = 2,
                Name = "Package B",
                Amount = 700,
                IsActive = false
            };

            var response = await client.DeleteAsync($"/api/TravelPackage/{packageToDelete.Id}");

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}