using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos
{
    /// <summary>
    /// Data Transfer Object for the SaleItem entity.
    /// </summary>
    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public string ProductExternalId { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Maps a SaleItem domain entity to a SaleItemDto.
        /// </summary>
        public static SaleItemDto FromEntity(SaleItem item)
        {
            if (item == null) return null;

            return new SaleItemDto
            {
                Id = item.Id,
                ProductExternalId = item.ProductExternalId,
                ProductDescription = item.ProductDescription,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                TotalAmount = item.TotalAmount,
                IsCancelled = item.IsCancelled
            };
        }
    }
}
