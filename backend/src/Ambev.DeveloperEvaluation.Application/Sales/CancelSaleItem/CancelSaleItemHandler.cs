using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, SaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CancelSaleItemHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetById(request.SaleId);

            var saleItem = sale.GetSaleItem(request.ProductId);

            saleItem.CancelItem();

            var saleUpdate = await _saleRepository.UpdateAsync(sale);

            var result = _mapper.Map<SaleResult>(saleUpdate);

            return result;
        }
    }
}
