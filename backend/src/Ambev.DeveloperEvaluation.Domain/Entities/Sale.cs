using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        protected Sale()
        {
            Items = new List<SaleItemVO>();
            SaleDate = DateTime.UtcNow;
        }

        public Sale(UserExternalVO saleUser, BranchExternalVO saleBranch, (ProductExternalVO product, int quantity)[] products) 
            : base()
        {
            this.SaleCustomer = new SaleCustomerVO(
                sale: this,
                customerName: saleUser.name,
                customerId: saleUser.userId,
                customerEmail: saleUser.email
               );

            this.Branch = new SaleBranchVO(
                sale: this,
                branchId: saleBranch.id,
                branchName: saleBranch.name);

            foreach (var item in products)
            {
                this.AddSaleItem(item.product, item.quantity);
            }

            this.SaleTotal = CalculateSaleTotal();
        }
        public DateTime SaleDate { get; protected set; }
        public decimal SaleTotal { get; protected set; }
     
        public SaleCustomerVO SaleCustomer { get; protected set; }
        public SaleBranchVO Branch { get; protected set; }
        public bool Cancelled { get; protected set; }
        public virtual ICollection<SaleItemVO> Items { get; protected set; }

        #region COMPUTED PROPERTIES
        public string SaleNumber => base.Id.ToString();
        public decimal ListPrice => Items.Sum(x => x.ListPrice);
        public string[] SaleProducts => Items.Select(x => x.ProductName).ToArray();
        #endregion

        #region BUSINESS RULES
        protected ushort GetSaleItemsDiscountPercentage(int productQuantity, decimal productPrice)
        {
            const ushort DISCOUNT_4_to_9_IDENTICAL = 10;
            const ushort DISCOUNT_10_to_20_IDENTICAL = 20;

            if (productQuantity >= 4 && productQuantity < 10)
                return DISCOUNT_4_to_9_IDENTICAL;

            if (productQuantity >= 10 && productQuantity <= 20)
                return DISCOUNT_10_to_20_IDENTICAL;

            return 0;
        }
        
        #endregion

        public ValidationResultDetail Validate()
        {
            throw new NotImplementedException();
        }


        public void UpdateSale(Sale sale)
        {
            this.Branch = sale.Branch;
            this.Items = sale.Items;
            this.SaleTotal = CalculateSaleTotal();
        }

        private void AddSaleItem(ProductExternalVO product, int productQuantity)
        {
            var saleDiscount = GetSaleItemsDiscountPercentage(productQuantity, product.price);
            var amountPrice = (product.price * productQuantity);
            var discountPrice = amountPrice * (saleDiscount / 100m);
            var totalPrice = amountPrice - discountPrice;

            var saleItem = 
                new SaleItemVO(
               sale: this,
               productId: product.id,
               productName: product.name,
               productCategory: product.description,
               productPrice: product.price,
               quantity: productQuantity,
               discountPercentage: saleDiscount,
               totalPrice: totalPrice);

            this.Items.Add(saleItem);
        }

        private decimal CalculateSaleTotal()
        {
            var total = this.Items.Sum(x => x.TotalPrice);

            return total;
        }
    }
}
