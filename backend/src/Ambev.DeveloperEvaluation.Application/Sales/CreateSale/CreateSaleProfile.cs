
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<ValidCreateSaleDTO, Sale>()
                .ConstructUsing(dto => 
                        new Sale(dto.customer, dto.branch, dto.products ));
        }
    }
}
