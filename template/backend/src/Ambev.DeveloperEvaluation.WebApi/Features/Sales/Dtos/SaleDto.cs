using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos
{
    /// <summary>
    /// Data Transfer Object for the Sale entity.
    /// </summary>
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerExternalId { get; set; }
        public string BranchExternalId { get; set; }
        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();

        /// <summary>
        /// Maps a Sale domain entity to a SaleDto.
        /// </summary>
        /// <param name="sale">The Sale entity.</param>
        /// <returns>A SaleDto with equivalent data.</returns>
        public static SaleDto FromEntity(Sale sale)
        {
            if (sale == null) return null;

            return new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                CustomerExternalId = sale.CustomerExternalId,
                BranchExternalId = sale.BranchExternalId,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(SaleItemDto.FromEntity).ToList()
            };
        }

        /// <summary>
        /// Applies this DTO's values to an existing Sale domain entity.
        /// </summary>
        /// <param name="sale">The Sale entity to update.</param>
        public void UpdateEntity(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));

            // Update basic fields
            sale.UpdateFrom(new Sale(
                sale.Id,
                SaleNumber,
                Date,
                CustomerExternalId,
                BranchExternalId
            ));

            if (IsCancelled)
                sale.Cancel();

            // Cancel existing items
            foreach (var existingItem in sale.Items.ToList())
            {
                sale.CancelItem(existingItem.Id);
            }

            // Add or update items from DTO
            foreach (var itemDto in Items)
            {
                var match = sale.Items.FirstOrDefault(i => i.Id == itemDto.Id);
                if (match != null)
                {
                    match.UpdateFrom(new SaleItem(
                        match.Id,
                        itemDto.ProductExternalId,
                        itemDto.ProductDescription,
                        itemDto.Quantity,
                        itemDto.UnitPrice,
                        itemDto.Discount
                    ));
                }
                else
                {
                    sale.AddItem(new SaleItem(
                        Guid.NewGuid(),
                        itemDto.ProductExternalId,
                        itemDto.ProductDescription,
                        itemDto.Quantity,
                        itemDto.UnitPrice,
                        itemDto.Discount
                    ));
                }
            }
        }
    }

}
