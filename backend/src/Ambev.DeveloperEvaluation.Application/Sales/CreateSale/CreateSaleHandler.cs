using System.Security.Cryptography;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductReadOnlyRepository _readOnlyProductRepository;
        private readonly IUserReadOnlyRepository _readOnlyUserRepository;
        private readonly IBranchReadOnlyRepository _readOnlyBranchRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(
            ISaleRepository saleRepository, 
            IProductReadOnlyRepository readOnlyProductRepository, 
            IUserReadOnlyRepository readOnlyUserRepository, 
            IBranchReadOnlyRepository readOnlyBranchRepository, 
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _readOnlyProductRepository = readOnlyProductRepository;
            _readOnlyUserRepository = readOnlyUserRepository;
            _readOnlyBranchRepository = readOnlyBranchRepository;
            _mapper = mapper;
        }

        public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            //var validator = new CreateSaleCommandValidator();
            //var validationResult = await validator.ValidateAsync(command, cancellationToken);

            //if (!validationResult.IsValid)
            //    throw new ValidationException(validationResult.Errors);

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

            //TODO: add cancellation token to repo
            var createdSale = await _saleRepository.CreateAsync(sale);
            var result = _mapper.Map<SaleResult>(createdSale);

            return result;
        }
    }
}
