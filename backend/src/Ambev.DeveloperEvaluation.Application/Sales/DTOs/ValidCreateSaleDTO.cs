
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public struct ValidCreateSaleDTO
    {
        public (ProductExternalQuery, int)[] products;
        public BranchExternalQuery branch;
        public UserExternalVO customer;

        public ValidCreateSaleDTO((ProductExternalQuery product, int quantity)[] products, BranchExternalQuery branch, UserExternalVO customer)
        {
            this.products = products;
            this.branch = branch;
            this.customer = customer;
        }
    }
}
