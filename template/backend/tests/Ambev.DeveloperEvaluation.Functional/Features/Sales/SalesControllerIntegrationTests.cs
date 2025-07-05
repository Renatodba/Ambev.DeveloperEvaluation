using System;
using System.Net.Http.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Ambev.DeveloperEvaluation.Functional.Features.Sales
{
    /// <summary>
    /// Integration tests for SalesController using in-memory TestServer.
    /// </summary>
    public class SalesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var solutionDir = Directory.GetParent(projectDir).FullName;
            var webApiPath = Path.Combine(solutionDir, "src", "Ambev.DeveloperEvaluation.WebApi");

            var clientFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot(webApiPath);
                builder.UseEnvironment(Environments.Development);
            });
            _client = clientFactory.CreateClient();
        }

        [Fact]
        public async Task PostAndGetSale_Should_Create_And_Retrieve_Sale()
        {
            // Arrange
            var createDto = new CreateSaleDto
            {
                SaleNumber = "INT-001",
                Date = DateTime.UtcNow,
                CustomerExternalId = "CUST-123",
                BranchExternalId = "BR-456",
                Items = new()
                {
                    new CreateSaleItemDto
                    {
                        ProductExternalId = "PRD-789",
                        ProductDescription = "Integration Test Product",
                        Quantity = 5,
                        UnitPrice = 20.0m,
                        Discount = 0m
                    }
                }
            };

            // Act: Create sale
            var postResponse = await _client.PostAsJsonAsync("/api/sales", createDto);

            // Assert: Created
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var created = await postResponse.Content.ReadFromJsonAsync<SaleDto>();
            created.Should().NotBeNull();
            created!.SaleNumber.Should().Be(createDto.SaleNumber);
            created.Items.Should().HaveCount(1);

            // Act: Retrieve the same sale
            var getResponse = await _client.GetAsync($"/api/sales/{created.Id}");

            // Assert: Found
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var fetched = await getResponse.Content.ReadFromJsonAsync<SaleDto>();
            fetched.Should().NotBeNull();
            fetched.Id.Should().Be(created.Id);
            fetched.SaleNumber.Should().Be(createDto.SaleNumber);
        }

        [Fact]
        public async Task GetNonexistentSale_Should_Return_NotFound()
        {
            // Act
            var randomId = Guid.NewGuid();
            var response = await _client.GetAsync($"/api/sales/{randomId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}