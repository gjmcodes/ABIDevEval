using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<SaleResult>
    {
        public Dictionary<Guid, int> ProductQuantity { get; set; }
        public Guid SaleBranchId { get; set; }
        public Guid UserId { get; set; }
    }

}
