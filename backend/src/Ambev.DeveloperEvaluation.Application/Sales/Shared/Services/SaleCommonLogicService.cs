
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Shared.Services
{

    public class SaleCommonLogicService
    {
        private readonly IMapper _mapper;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IBranchReadOnlyRepository _branchReadOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;

        public SaleCommonLogicService(IMapper mapper, 
            IUserReadOnlyRepository userReadOnlyRepository, 
            IBranchReadOnlyRepository branchReadOnlyRepository, 
            IProductReadOnlyRepository productReadOnlyRepository)
        {
            _mapper = mapper;
            _userReadOnlyRepository = userReadOnlyRepository;
            _branchReadOnlyRepository = branchReadOnlyRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
        }

        public async Task<ValidCreateSaleDTO> GetValidSaleDataAsync(Guid userId, Guid branchId, Dictionary<Guid, int> productQuantity)
        {
            var userTask = _userReadOnlyRepository.GetById(userId);
            var productsTask = _productReadOnlyRepository.GetAllByIds(productQuantity.Keys.ToArray());
            var branchTask = _branchReadOnlyRepository.GetById(branchId);
            await Task.WhenAll(userTask, productsTask, branchTask);

            if (userTask.Result == null)
                throw new InvalidOperationException($"User {userId} not found");

            if (branchTask.Result == null)
                throw new InvalidOperationException($"Branch {branchId} not found");

            var productQueryQuantity = new List<(ProductExternalQuery product, int quantity)>();
            var _lock = new object();

            Parallel.ForEach(productsTask.Result, productQuery =>
            {
                var prodId = productQuery.GuidId;

                if (!productQuantity.ContainsKey(prodId))
                {
                    throw new InvalidOperationException($"Product {prodId} not found");
                }
                lock (_lock)
                {
                    productQueryQuantity.Add(new(productQuery, productQuantity[prodId]));
                }
            });

            var validCommand = new ValidCreateSaleDTO(productQueryQuantity.ToArray(), branchTask.Result, userTask.Result);

            return validCommand;
        }
    }
}
