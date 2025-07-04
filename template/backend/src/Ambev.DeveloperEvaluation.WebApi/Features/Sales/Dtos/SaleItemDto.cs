namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos
{
    public class SaleItemDto
    {
        public string ProductExternalId { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
