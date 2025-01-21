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

        public DateTime SaleDate { get; protected set; }
        public decimal SaleTotal { get; protected set; }
     
        public SaleCustomerVO SaleCustomer { get; protected set; }
        public SaleBranchVO Branch { get; protected set; }
        public int DiscountPercentage { get; protected set; }
        public bool Cancelled { get; protected set; }
        public virtual ICollection<SaleItemVO> Items { get; protected set; }


        //Computed properties
        public string SaleNumber => base.Id.ToString();
        public decimal ListPrice => Items.Sum(x => x.Total);
        public string[] SaleProducts => Items.Select(x => x.ProductName).ToArray();

        public ValidationResultDetail Validate()
        {
            throw new NotImplementedException();
        }

        public void ApplyDiscounts4To9IdenticalItems()
        {
            throw new NotImplementedException();
        }

        public void ApplyDiscounts10To20IdenticalItems()
        {
            throw new NotImplementedException();
        }
    }
}
