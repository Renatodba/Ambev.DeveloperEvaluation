using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Features.Sales.Services
{
    /// <summary>
    /// Concrete implementation of ISaleService using ISaleRepository.
    /// Applies business rules for discounts and quantity limits.
    /// </summary>
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repo;

        public SaleService(ISaleRepository repo) => _repo = repo;

        /// <inheritdoc />
        public async Task<SaleDto> CreateAsync(CreateSaleDto dto)
        {
            // Business rule: maximum 20 identical items
            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 identical items.");
            }

            // Instantiate sale
            var sale = new Sale(
                Guid.NewGuid(),
                dto.SaleNumber,
                dto.Date,
                dto.CustomerExternalId,
                dto.BranchExternalId
            );

            // Add items with quantity-based discount
            foreach (var itemDto in dto.Items)
            {
                var discount = itemDto.Quantity >= 10 ? 0.20m
                                : itemDto.Quantity >= 4 ? 0.10m
                                : 0m;
                var item = new SaleItem(
                    Guid.NewGuid(),
                    itemDto.ProductExternalId,
                    itemDto.ProductDescription,
                    itemDto.Quantity,
                    itemDto.UnitPrice,
                    discount
                );
                sale.AddItem(item);
            }

            await _repo.CreateAsync(sale);
            return SaleDto.FromEntity(sale);
        }

        /// <inheritdoc />
        public async Task<SaleDto> UpdateAsync(Guid id, UpdateSaleDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            // Business rule: maximum 20 identical items
            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 identical items.");
            }

            // Update core sale data
            existing.UpdateFrom(new Sale(
                id,
                dto.SaleNumber,
                dto.Date,
                dto.CustomerExternalId,
                dto.BranchExternalId
            ));
            existing.Cancel(); // if needed

            // Remove old items
            foreach (var oldItem in existing.Items.ToList())
                existing.CancelItem(oldItem.Id);

            // Add updated items with discount logic
            foreach (var itemDto in dto.Items)
            {
                var discount = itemDto.Quantity >= 10 ? 0.20m
                                : itemDto.Quantity >= 4 ? 0.10m
                                : 0m;
                var newItem = new SaleItem(
                    Guid.NewGuid(),
                    itemDto.ProductExternalId,
                    itemDto.ProductDescription,
                    itemDto.Quantity,
                    itemDto.UnitPrice,
                    discount
                );
                existing.AddItem(newItem);
            }

            await _repo.UpdateAsync(existing);
            return SaleDto.FromEntity(existing);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        /// <inheritdoc />
        public async Task<SaleDto> GetByIdAsync(Guid id)
        {
            var sale = await _repo.GetByIdAsync(id);
            return sale == null ? null : SaleDto.FromEntity(sale);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SaleDto>> GetAllAsync()
        {
            var sales = await _repo.GetAllAsync();
            return sales.Select(SaleDto.FromEntity);
        }
    }
}