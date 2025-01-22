using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleResult>
    {

        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IBranchReadOnlyRepository _branchReadOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;

        public UpdateSaleHandler(
            IMapper mapper, 
            ISaleRepository saleRepository, 
            IUserReadOnlyRepository userReadOnlyRepository, 
            IBranchReadOnlyRepository branchReadOnlyRepository, 
            IProductReadOnlyRepository productReadOnlyRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _branchReadOnlyRepository = branchReadOnlyRepository;
            _productReadOnlyRepository = productReadOnlyRepository;
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

            return result;
        }
    }
}
