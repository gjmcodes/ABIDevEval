using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<ValidCreateSaleDTO, Sale>()
                .ConstructUsing(dto =>
                        new Sale(dto.customer, dto.branch, dto.products));
        }
    }
}
