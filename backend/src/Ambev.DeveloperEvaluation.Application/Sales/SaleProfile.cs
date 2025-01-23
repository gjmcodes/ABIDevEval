using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
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
            CreateMap<Sale, SaleResult>();
            CreateMap<SaleBranchVO, SaleBranchResult>();
            CreateMap<SaleCustomerVO, SaleCustomerResult>();
            CreateMap<SaleItemVO, SaleItemResult>();

            CreateMap<SaleResult, SaleQuery>();
            CreateMap<SaleBranchResult, SaleBranchQuery>();
            CreateMap<SaleCustomerResult, SaleCustomerQuery>();
            CreateMap<SaleItemResult, SaleItemQuery>();
        }
    }
}
