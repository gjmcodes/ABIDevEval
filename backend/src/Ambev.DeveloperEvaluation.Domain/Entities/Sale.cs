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
        }

        public DateTime SaleDate { get; private set; }
        public decimal SaleTotal { get; private set; }
     
        public SaleCustomerVO SaleCustomer { get; private set; }
        public SaleBranchVO Branch { get; private set; }
        public int DiscountPercentage { get; private set; }
        public bool Cancelled { get; private set; }
        public virtual ICollection<SaleItemVO> Items { get; private set; }


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
