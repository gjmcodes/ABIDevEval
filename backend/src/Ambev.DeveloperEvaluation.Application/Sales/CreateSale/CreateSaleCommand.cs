using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Dictionary<Guid, int> ProductQuantity { get; set; }
        public Guid SaleBranchId { get; set; }
        public Guid UserId { get; set; }

    }

}
