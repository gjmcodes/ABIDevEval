using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public SaleProductDTO[] Products { get; set; }
        public Guid SaleBranch { get; set; }
        public Guid UserId { get; set; }

    }

}
