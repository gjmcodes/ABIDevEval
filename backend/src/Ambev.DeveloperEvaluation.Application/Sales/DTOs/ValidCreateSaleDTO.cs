
using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public struct ValidCreateSaleDTO
    {
        public (ProductExternalQuery, int)[] products;
        public BranchExternalQuery branch;
        public UserExternalQuery customer;

        public ValidCreateSaleDTO((ProductExternalQuery product, int quantity)[] products, BranchExternalQuery branch, UserExternalQuery customer)
        {
            this.products = products;
            this.branch = branch;
            this.customer = customer;
        }
    }
}
