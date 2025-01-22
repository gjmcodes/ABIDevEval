using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
    {
        public CancelSaleItemRequestValidator()
        {
            RuleFor(sale => sale.SaleId).NotNull().NotEmpty().WithMessage("Sale Id cannot be empty");
            RuleFor(sale => sale.ProductId).NotNull().NotEmpty().WithMessage("Product Id cannot be empty");

        }
    }
}
