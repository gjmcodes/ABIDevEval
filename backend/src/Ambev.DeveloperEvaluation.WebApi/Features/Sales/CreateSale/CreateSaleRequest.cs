namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public CreateSaleRequest()
        {
            ProductQuantity = new Dictionary<Guid, int>();
        }
        public Dictionary<Guid, int> ProductQuantity { get; set; }
        public Guid SaleBranchId { get; set; }
        public Guid UserId { get; set; }
    }
}
