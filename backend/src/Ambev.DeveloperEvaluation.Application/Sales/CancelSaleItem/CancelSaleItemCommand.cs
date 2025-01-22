
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemCommand : IRequest<SaleResult>
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public bool Cancelled { get; set; }
    }
}
