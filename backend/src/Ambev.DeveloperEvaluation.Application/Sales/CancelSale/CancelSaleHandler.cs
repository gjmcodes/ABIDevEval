﻿
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, SaleResult>
    {

        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public CancelSaleHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<SaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetById(request.Id);
            if(sale == null)
                throw new InvalidOperationException($"Sale {request.Id} not found");

            sale.CancelSale();

            var saleUpdate = await _saleRepository.UpdateAsync(request.Id, sale);
            var result = _mapper.Map<SaleResult>(saleUpdate);

            return result;
        }
    }
}
