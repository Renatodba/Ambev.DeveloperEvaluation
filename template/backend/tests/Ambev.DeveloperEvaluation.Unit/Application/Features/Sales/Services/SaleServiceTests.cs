using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Features.Sales.Services
{
    /// <summary>
    /// Unit tests for <see cref="SaleService"/>.
    /// </summary>
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _repoMock;
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _repoMock = new Mock<ISaleRepository>();
            _service = new SaleService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Call_CreateAsync_On_Repository_And_Return_Dto()
        {
            // Arrange
            var dto = new CreateSaleDto
            {
                SaleNumber = "S-001",
                Date = DateTime.UtcNow,
                CustomerExternalId = "C1",
                BranchExternalId = "B1",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        ProductExternalId = "P1",
                        ProductDescription = "Test Product",
                        Quantity = 5,
                        UnitPrice = 10m,
                        Discount = 0.1m
                    }
                }
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            _repoMock.Verify(r => r.CreateAsync(It.IsAny<Sale>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(dto.SaleNumber, result.SaleNumber);
            Assert.Single(result.Items);
            Assert.Equal(5, result.Items[0].Quantity);
        }

        [Fact]
        public async Task GetAllAsync_Should_Call_GetAllAsync_On_Repository_And_Return_List()
        {
            // Arrange
            var salesList = new List<Sale>
            {
                new Sale(Guid.NewGuid(), "S1", DateTime.UtcNow, "C1", "B1"),
                new Sale(Guid.NewGuid(), "S2", DateTime.UtcNow, "C2", "B2")
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(salesList);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
            Assert.Equal(2, ((List<SaleDto>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_Nonexistent_Should_Return_Null()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null);

            // Act
            var result = await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
            _repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_DeleteAsync_On_Repository()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}

