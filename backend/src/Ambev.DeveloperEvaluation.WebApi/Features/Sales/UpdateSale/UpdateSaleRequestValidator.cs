using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(sale => sale.Id).NotNull().NotEmpty().WithMessage("Sale Id cannot be empty");
            RuleFor(sale => sale.SaleBranchId).NotNull().NotEmpty().WithMessage("Sale Branch Id cannot be empty");
            RuleFor(sale => sale.UserId).NotNull().NotEmpty().WithMessage("User Id cannot be empty");
            RuleFor(sale => sale.ProductQuantity.Count).GreaterThan(0).WithMessage("Prodcuts cannot be empty");
            RuleFor(sale => sale.ProductQuantity.Count).LessThanOrEqualTo(20).WithMessage("A sale cannot have more than 20 identical products");
        }
    }
}
