using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemProfile : Profile
    {
        public CancelSaleItemProfile()
        {
            CreateMap<CancelSaleItemRequest, CancelSaleItemCommand>();

        }
    }
}
