using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository for sales, providing CRUD operations for the Sale entity.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Inserts a new sale into the database.
    /// </summary>
    /// <param name="sale">The Sale entity to insert.</param>
    /// <returns>The inserted Sale, including its generated Id.</returns>
    Task<Sale> CreateAsync(Sale sale);

    /// <summary>
    /// Updates an existing sale in the database.
    /// </summary>
    /// <param name="sale">The Sale entity with updated data.</param>
    Task UpdateAsync(Sale sale);

    /// <summary>
    /// Deletes a sale by its identifier.
    /// </summary>
    /// <param name="saleId">The Id of the sale to delete.</param>
    Task DeleteAsync(Guid saleId);

    /// <summary>
    /// Retrieves a sale by its identifier.
    /// </summary>
    /// <param name="saleId">The Id of the sale to retrieve.</param>
    /// <returns>The Sale entity, or null if not found.</returns>
    Task<Sale> GetByIdAsync(Guid saleId);

    /// <summary>
    /// Retrieves all sales from the database.
    /// </summary>
    /// <returns>A list of all Sale entities.</returns>
    Task<IEnumerable<Sale>> GetAllAsync();
}
