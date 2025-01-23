using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Shared.Results;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ISaleReadOnlyRepository _saleReadOnlyRepository;
        public SalesController(IMediator mediator, IMapper mapper, ISaleReadOnlyRepository saleReadOnlyRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _saleReadOnlyRepository = saleReadOnlyRepository;
        }


        /// <summary>
        /// Get a sale
        /// </summary>
        /// <param name="request">The sale requested</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details</returns>
        [HttpGet("{saleId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleQuery>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromRoute] string saleId, CancellationToken cancellationToken)
        {
            Guid saleGuid;
            var valid = Guid.TryParse(saleId, out saleGuid);
            if (!valid)
                return BadRequest("Sale Id not valid");

            var sale = await _saleReadOnlyRepository.GetById(saleGuid);

            if (sale == null)
                return NotFound("Sale not found");

            return Created(string.Empty, new ApiResponseWithData<SaleQuery>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = sale
            });
        }

        /// <summary>
        /// Retrieves all sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sales details if found</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductExternalQuery[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var sales = await _saleReadOnlyRepository.GetAll();

            if (sales == null || sales.Count() == 0)
                return NotFound("No sales found");

            return Ok(sales.ToArray());
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="request">The sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = response
            });
        }

        /// <summary>
        /// Cancels a sale
        /// </summary>
        /// <param name="request">The sale cancellment request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelSale([FromBody] CancelSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Message = "Sale cancelled successfully",
                Data = response
            });
        }

        /// <summary>
        /// Alters a sale
        /// </summary>
        /// <param name="request">The sale update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale details</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Message = "Sale item cancelled successfully",
                Data = response
            });
        }

        /// <summary>
        /// Cancels a sale item
        /// </summary>
        /// <param name="request">The sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        [HttpPatch("{saleId}/CancelSaleItem/")]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelSale([FromBody] CancelSaleItemRequest request, [FromRoute] string saleId, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CancelSaleItemCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Message = "Sale item cancelled successfully",
                Data = response
            });
        }
    }
}
