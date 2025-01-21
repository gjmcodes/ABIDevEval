
namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public struct SaleCustomerVO
    {
        public SaleCustomerVO(Guid saleId, string customerName, Guid customerId)
        {
            SaleId = saleId;
            CustomerName = customerName;
            CustomerId = customerId;
        }

        public Guid SaleId { get; }
        public string CustomerName { get; }
        public Guid CustomerId { get; }
    }
}
