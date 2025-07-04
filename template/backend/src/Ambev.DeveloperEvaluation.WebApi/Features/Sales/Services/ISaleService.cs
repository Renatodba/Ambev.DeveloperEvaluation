using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services
{
    public interface ISaleService
    {
        Task<SaleDto> CreateAsync(CreateSaleDto dto);
        Task<SaleDto> UpdateAsync(Guid id, UpdateSaleDto dto);
        Task DeleteAsync(Guid id);
        Task<SaleDto> GetByIdAsync(Guid id);
        Task<IEnumerable<SaleDto>> GetAllAsync();
    }
}
