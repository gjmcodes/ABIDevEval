
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs
{
    public struct ValidCreateSaleDTO
    {
        public (ProductExternalVO, int)[] products;
        public BranchExternalVO branch;
        public UserExternalVO customer;

        public ValidCreateSaleDTO((ProductExternalVO product, int quantity)[] products, BranchExternalVO branch, UserExternalVO customer)
        {
            this.products = products;
            this.branch = branch;
            this.customer = customer;
        }
    }
}
