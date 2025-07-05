using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services
{
    /// <summary>
    /// Implementation of <see cref="ISaleService"/> using <see cref="ISaleRepository"/>.
    /// </summary>
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleService"/> class.
        /// </summary>
        /// <param name="repo">The sale repository.</param>
        public SaleService(ISaleRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public async Task<SaleDto> CreateAsync(CreateSaleDto dto)
        {
            var sale = new Sale(
                Guid.NewGuid(),
                dto.SaleNumber,
                dto.Date,
                dto.CustomerExternalId,
                dto.BranchExternalId
            );

            foreach (var itemDto in dto.Items)
            {
                var item = new SaleItem(
                    Guid.NewGuid(),
                    itemDto.ProductExternalId,
                    itemDto.ProductDescription,
                    itemDto.Quantity,
                    itemDto.UnitPrice,
                    itemDto.Discount
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
            if (existing == null)
                return null;

            existing.UpdateFrom(new Sale(
                id,
                dto.SaleNumber,
                dto.Date,
                dto.CustomerExternalId,
                dto.BranchExternalId
            ));

            existing.Cancel();
            foreach (var item in existing.Items.ToList())
                existing.CancelItem(item.Id);

            foreach (var itemDto in dto.Items)
            {
                var item = new SaleItem(
                    Guid.NewGuid(),
                    itemDto.ProductExternalId,
                    itemDto.ProductDescription,
                    itemDto.Quantity,
                    itemDto.UnitPrice,
                    itemDto.Discount
                );
                existing.AddItem(item);
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
