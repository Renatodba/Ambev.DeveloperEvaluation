using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services
{
    public class SaleService: ISaleService
    {
        public async Task<SaleDto> CreateAsync(CreateSaleDto dto)
        {
            var sale = new Sale(Guid.NewGuid(), dto.SaleNumber, dto.Date, dto.CustomerExternalId, dto.BranchExternalId);
            foreach (var i in dto.Items)
                sale.AddItem(new SaleItem(
                    Guid.NewGuid(), i.ProductExternalId, i.ProductDescription,
                    i.Quantity, i.UnitPrice, i.Discount
                ));
            await _repo.InsertAsync(sale);
            return SaleDto.FromEntity(sale);
        }

        //TODO: Implementar UpdateAsync, DeleteAsync, GetByIdAsync, GetAllAsync...
    }

}
