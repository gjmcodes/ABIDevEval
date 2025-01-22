
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class SaleItemVO
    {
        private SaleItemVO(){}

        public SaleItemVO(Sale sale, Guid productId, 
            string productName, 
            string productCategory, 
            decimal productPrice, 
            int quantity,
            ushort discountPercentage,
            decimal totalPrice
            )
        {
            Sale = sale;
            SaleId = sale.Id;
            ProductId = productId;
            ProductName = productName;
            ProductCategory = productCategory;
            ProductPrice = productPrice;
            Quantity = quantity;
            DiscountPercentage = discountPercentage;
            TotalPrice = totalPrice;
        }

        public virtual Sale Sale { get; private set; }
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductCategory { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int Quantity { get; private set; }
        public ushort DiscountPercentage { get; private set; }
        public decimal TotalPrice { get; private set; }

        //Computed properties
        public decimal ListPrice => ProductPrice * Quantity;

    }
}
