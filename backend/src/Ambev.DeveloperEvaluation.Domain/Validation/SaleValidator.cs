
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Shared.Validations
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(x => x.SaleCustomer.CustomerId)
              .NotEqual(Guid.Empty)
              .WithMessage("A sale requires an user");

            RuleFor(x => x.SaleBranch.BranchId)
              .NotEqual(Guid.Empty)
              .WithMessage("A sale requires a branch");

            RuleFor(x => x.Items.Count)
              .GreaterThan(0)
              .WithMessage("A sale requires products");
            RuleFor(x => x.Items.Count)
               .GreaterThan(0)
               .WithMessage("A sale requires products");

            RuleForEach(x => x.Items)
                .ChildRules(child =>
                {
                    child.RuleFor(c => c.Quantity).GreaterThan(0).WithMessage("A sale cannot have products with 0 of quantity");
                    child.RuleFor(c => c.Quantity).LessThanOrEqualTo(20).WithMessage("A sale cannot have more than 20 identical products");
                });
        }
    }
}
