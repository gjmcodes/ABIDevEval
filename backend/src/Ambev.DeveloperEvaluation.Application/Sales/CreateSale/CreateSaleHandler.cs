using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.BUS;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductReadOnlyRepository _readOnlyProductRepository;
        private readonly IUserReadOnlyRepository _readOnlyUserRepository;
        private readonly IBranchReadOnlyRepository _readOnlyBranchRepository;
        private readonly ISaleReadOnlyRepository _saleReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IBUS _bus;

        public CreateSaleHandler(
            ISaleRepository saleRepository, 
            IProductReadOnlyRepository readOnlyProductRepository, 
            IUserReadOnlyRepository readOnlyUserRepository, 
            IBranchReadOnlyRepository readOnlyBranchRepository,
            ISaleReadOnlyRepository saleReadOnlyRepository,
            IMapper mapper,
            IBUS bus)
        {
            _saleRepository = saleRepository;
            _readOnlyProductRepository = readOnlyProductRepository;
            _readOnlyUserRepository = readOnlyUserRepository;
            _readOnlyBranchRepository = readOnlyBranchRepository;
            _saleReadOnlyRepository = saleReadOnlyRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var userTask = _readOnlyUserRepository.GetById(command.UserId);
            var productsTask = _readOnlyProductRepository.GetAllByIds(command.ProductQuantity.Keys.ToArray());
            var branchTask = _readOnlyBranchRepository.GetById(command.SaleBranchId);

            await Task.WhenAll(userTask, productsTask, branchTask);

            if(userTask.Result == null)
                throw new InvalidOperationException($"User {command.UserId} not found");

            if(branchTask.Result == null)
                throw new InvalidOperationException($"Branch {command.SaleBranchId} not found");

            var productQueryQuantity = new List<(ProductExternalQuery product, int quantity)>();
            var _lock = new object();

            Parallel.ForEach(productsTask.Result, productQuery =>
            {
                var prodId = productQuery.GuidId;

                if (!command.ProductQuantity.ContainsKey(prodId))
                {
                    throw new InvalidOperationException($"Product {prodId} not found");
                }
                lock (_lock)
                {
                    productQueryQuantity.Add(new(productQuery, command.ProductQuantity[prodId]));
                }
            });

            var validCommand = new ValidCreateSaleDTO(productQueryQuantity.ToArray(), branchTask.Result, userTask.Result);


            var sale = _mapper.Map<Sale>(validCommand);
            var saleValidation = sale.Validate();
            if (!saleValidation.IsValid)
            {
                throw new InvalidOperationException(string.Join(Environment.NewLine, saleValidation.Errors));
            }

            //TODO: add cancellation token to repo
            var createdSale = await _saleRepository.CreateAsync(sale);
            var result = _mapper.Map<SaleResult>(createdSale);

            var readOnlyData = _mapper.Map<SaleQuery>(result);
           var readonlyResult = await _saleReadOnlyRepository.CreateReadOnlyData(readOnlyData);
            if(!readonlyResult)
                throw new InvalidOperationException($"Error while updating readonly sales data");

            var @event = new BusEvent<SaleResult>()
            {
                eventTime = DateTime.UtcNow,
                eventType = SalesEvents.SaleCreated.ToString(),
                value = result
            };

            await _bus.SendEvent(@event);

            return result;
        }
    }
}
