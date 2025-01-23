using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Services;
using Ambev.DeveloperEvaluation.BUS;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
    {

        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IBranchReadOnlyRepository _branchReadOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;
        private readonly ISaleReadOnlyRepository _saleReadOnlyRepository;
        private readonly IBUS _bus;
        public UpdateSaleHandler(
            IMapper mapper, 
            ISaleRepository saleRepository, 
            IUserReadOnlyRepository userReadOnlyRepository, 
            IBranchReadOnlyRepository branchReadOnlyRepository, 
            IProductReadOnlyRepository productReadOnlyRepository,
            ISaleReadOnlyRepository saleReadOnlyRepository,
             IBUS bus)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _branchReadOnlyRepository = branchReadOnlyRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
            _saleReadOnlyRepository = saleReadOnlyRepository;
            _bus = bus;
        }

        public async Task<SaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sharedLogic = new SaleCommonLogicService(
                _mapper,
                _userReadOnlyRepository,
                _branchReadOnlyRepository,
                _productReadOnlyRepository);

            var validSale = await sharedLogic.GetValidSaleDataAsync(request.UserId, request.SaleBranchId, request.ProductQuantity);

            var sale = _mapper.Map<Sale>(validSale);

            var saleValidation = sale.Validate();
            if(!saleValidation.IsValid)
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, saleValidation.Errors));
            }
            var createdSale = await _saleRepository.UpdateAsync(request.Id, sale);
            var result = _mapper.Map<SaleResult>(createdSale);


            var readOnlyData = _mapper.Map<SaleQuery>(result);
            var readonlyResult = await _saleReadOnlyRepository.UpdateReadOnlyData(readOnlyData);
            if (!readonlyResult)
                throw new InvalidOperationException($"Error while updating readonly sales data");


            var @event = new BusEvent<SaleResult>()
            {
                eventTime = DateTime.UtcNow,
                eventType = SalesEvents.SaleModified.ToString(),
                value = result
            };

            await _bus.SendEvent(@event);

            return result;
        }
    }
}
