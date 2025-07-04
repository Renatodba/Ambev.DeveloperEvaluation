namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public string CustomerExternalId { get; set; } = null!;
        public string BranchExternalId { get; set; } = null!;
        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();

        
    }

}
