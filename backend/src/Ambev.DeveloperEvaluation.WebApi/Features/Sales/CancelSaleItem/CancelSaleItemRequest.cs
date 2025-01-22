namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public struct CancelSaleItemRequest
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
    }
}
