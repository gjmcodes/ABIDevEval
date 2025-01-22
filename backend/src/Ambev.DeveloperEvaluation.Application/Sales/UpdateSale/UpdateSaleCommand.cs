
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<SaleResult>
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, int> ProductQuantity { get; set; }
        public Guid SaleBranchId { get; set; }
        public Guid UserId { get; set; }
    }
}
