namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos
{
    public class CreateSaleDto
    {
        public string SaleNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public string CustomerExternalId { get; set; } = null!;
        public string BranchExternalId { get; set; } = null!;
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
