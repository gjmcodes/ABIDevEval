﻿using Ambev.DeveloperEvaluation.Application.Sales.Shared.Validations;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        protected Sale()
        {
        }

        public Sale(UserExternalQuery saleCustomer, BranchExternalQuery saleBranch, (ProductExternalQuery product, int quantity)[] products) 
            : base()
        {
            this.SaleCustomer = new SaleCustomerVO(
                sale: this,
                customerName: saleCustomer.Name,
                customerId: saleCustomer.GuidId,
                customerEmail: saleCustomer.Email
               );

            this.SaleBranch = new SaleBranchVO(
                sale: this,
                branchId: saleBranch.GuidId,
                branchName: saleBranch.Name);

            this.Items = new List<SaleItemVO>();

            foreach (var item in products)
            {
                this.AddSaleItem(item.product, item.quantity);
            }

            this.SaleTotal = CalculateSaleTotal();
            this.SaleDate = DateTime.UtcNow;
        }
        public DateTime SaleDate { get; protected set; }
        public DateTime? AlteredDate { get; protected set; }
        public decimal SaleTotal { get; protected set; }
     
        public SaleCustomerVO SaleCustomer { get; protected set; }
        public SaleBranchVO SaleBranch { get; protected set; }
        public bool Cancelled { get; protected set; }
        public virtual ICollection<SaleItemVO> Items { get; protected set; }

        #region COMPUTED PROPERTIES
        public string SaleNumber => base.Id.ToString();
        public decimal ListPrice => Items.Sum(x => x.ListPrice);
        public string[] SaleProducts => Items.Select(x => x.ProductName).ToArray();
        #endregion

        private void AddSaleItem(ProductExternalQuery product, int productQuantity)
        {
            var saleDiscount = GetSaleItemsDiscountPercentage(productQuantity, product.Price);
            var amountPrice = (product.Price * productQuantity);
            var discountPrice = amountPrice * (saleDiscount / 100m);
            var totalPrice = amountPrice - discountPrice;

            var saleItem =
                new SaleItemVO(
               sale: this,
               productId: product.GuidId,
               productName: product.Name,
               productCategory: product.Category,
               productPrice: product.Price,
               quantity: productQuantity,
               discountPercentage: saleDiscount,
               totalPrice: totalPrice);

            this.Items.Add(saleItem);
        }

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
       
        private decimal CalculateSaleTotal()
        {
            var total = this.Items.Sum(x => x.TotalPrice);

            return total;
        }
        #endregion

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }


        public void UpdateSale(Sale sale)
        {
            this.SaleBranch = sale.SaleBranch;
            this.Items = sale.Items;
            this.SaleTotal = CalculateSaleTotal();
            this.AlteredDate = DateTime.UtcNow;
        }
        public void CancelSale()
        {
            this.Cancelled = true;

            foreach (var item in this.Items)
            {
                item.CancelItem();
            }
        }

        public SaleItemVO GetSaleItem(Guid productId)
        {
            var saleItem = Items.FirstOrDefault(x => x.ProductId == productId);
            return saleItem;
        }

       
    }
}
