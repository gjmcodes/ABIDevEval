using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.BUS;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, SaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleReadOnlyRepository _saleReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IBUS _bus;

        public CancelSaleItemHandler(ISaleRepository saleRepository,
            IMapper mapper,
            IBUS bus,
            ISaleReadOnlyRepository saleReadOnlyRepository)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _bus = bus;
            _saleReadOnlyRepository = saleReadOnlyRepository;
        }

        public async Task<SaleResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetById(request.SaleId);

            var saleItem = sale.GetSaleItem(request.ProductId);

            saleItem.CancelItem();

            var saleUpdate = await _saleRepository.UpdateAsync(sale);

            var result = _mapper.Map<SaleResult>(saleUpdate);

            var readOnlyData = _mapper.Map<SaleQuery>(result);
            var readonlyResult = await _saleReadOnlyRepository.UpdateReadOnlyData(readOnlyData);
            if (!readonlyResult)
                throw new InvalidOperationException($"Error while updating readonly sales data");

            var @event = new BusEvent<SaleResult>()
            {
                eventTime = DateTime.UtcNow,
                eventType = SalesEvents.ItemCancelled.ToString(),
                value = result
            };

            await _bus.SendEvent(@event);
            return result;
        }
    }
}
