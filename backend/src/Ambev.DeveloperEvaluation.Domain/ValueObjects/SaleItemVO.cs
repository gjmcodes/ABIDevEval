
namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class SaleItemVO
    {

        public Guid SaleId { get; }
        public Guid ProductId { get; }
        public string ProductName { get; }
        public string ProductCategory { get; }
        public decimal ProductPrice { get; }
        public int Quantity { get; }
        public decimal Total => ProductPrice * Quantity;
    }
}
