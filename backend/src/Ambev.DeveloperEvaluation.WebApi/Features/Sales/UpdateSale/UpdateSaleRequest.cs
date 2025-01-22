namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public UpdateSaleRequest()
        {
            ProductQuantity = new Dictionary<Guid, int>();
        }

        public Guid Id { get; set; }
        public Dictionary<Guid, int> ProductQuantity { get; set; }
        public Guid SaleBranchId { get; set; }
        public Guid UserId { get; set; }
    }
}
