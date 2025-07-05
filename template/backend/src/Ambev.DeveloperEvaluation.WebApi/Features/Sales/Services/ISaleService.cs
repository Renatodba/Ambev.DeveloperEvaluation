using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services
{
    /// <summary>
    /// Application service for managing sales (CRUD operations and business logic orchestration).
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="dto">Sale creation DTO.</param>
        /// <returns>The created Sale DTO.</returns>
        Task<SaleDto> CreateAsync(CreateSaleDto dto);

        /// <summary>
        /// Updates an existing sale.
        /// </summary>
        /// <param name="id">Identifier of the sale.</param>
        /// <param name="dto">Sale update DTO.</param>
        /// <returns>The updated Sale DTO, or null if not found.</returns>
        Task<SaleDto> UpdateAsync(Guid id, UpdateSaleDto dto);

        /// <summary>
        /// Deletes a sale by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the sale to delete.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Retrieves a sale by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the sale to retrieve.</param>
        /// <returns>The Sale DTO, or null if not found.</returns>
        Task<SaleDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>A list of Sale DTOs.</returns>
        Task<IEnumerable<SaleDto>> GetAllAsync();
    }
}