﻿using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.SaleBranchId).NotNull().NotEmpty().WithMessage("Sale Branch Id cannot be empty");
            RuleFor(sale => sale.UserId).NotNull().NotEmpty().WithMessage("User Id cannot be empty");
            RuleFor(sale => sale.ProductQuantity.Count).GreaterThan(0).WithMessage("Prodcuts cannot be empty");
            RuleFor(sale => sale.ProductQuantity.Count).LessThanOrEqualTo(20).WithMessage("A sale cannot have more than 20 identical products");


        }
    }
}
