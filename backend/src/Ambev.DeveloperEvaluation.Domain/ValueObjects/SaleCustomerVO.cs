
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class SaleCustomerVO
    {
        private SaleCustomerVO() { }

        public SaleCustomerVO(Sale sale, string customerName, Guid customerId, string customerEmail)
        {
            SaleId = sale.Id;
            CustomerName = customerName;
            CustomerId = customerId;
            CustomerEmail = customerEmail;
        }

        public Guid SaleId { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
    }
}
