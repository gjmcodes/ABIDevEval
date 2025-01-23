
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.BUS;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, SaleResult>
    {

        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleReadOnlyRepository _saleReadOnlyRepository;
        private readonly IBUS _bus;
        public CancelSaleHandler(
            IMapper mapper,
            ISaleRepository saleRepository,
            IBUS bus,
            ISaleReadOnlyRepository saleReadOnlyRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _bus = bus;
            _saleReadOnlyRepository = saleReadOnlyRepository;
        }

        public async Task<SaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetById(request.Id);
            if(sale == null)
                throw new InvalidOperationException($"Sale {request.Id} not found");

            sale.CancelSale();

            var saleUpdate = await _saleRepository.UpdateAsync(sale);
            var result = _mapper.Map<SaleResult>(saleUpdate);


            var readOnlyData = _mapper.Map<SaleQuery>(result);
            var readonlyResult = await _saleReadOnlyRepository.UpdateReadOnlyData(readOnlyData);
            if (!readonlyResult)
                throw new InvalidOperationException($"Error while updating readonly sales data");


            var @event = new BusEvent<SaleResult>()
            {
                eventTime = DateTime.UtcNow,
                eventType = SalesEvents.SaleCancelled.ToString(),
                value = result
            };

            await _bus.SendEvent(@event);

            return result;
        }
    }
}
