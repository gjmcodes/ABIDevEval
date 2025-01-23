using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IProductReadOnlyRepository _productReadOnlyRepository;

        public ProductsController(IProductReadOnlyRepository productReadOnlyRepository)
        {
            _productReadOnlyRepository = productReadOnlyRepository;
        }


        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The products details if found</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductExternalQuery[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var products = await _productReadOnlyRepository.GetAll();

            if (products == null || products.Count() == 0)
                return NotFound("No products found");

            return Ok( products.ToArray());
        }
    }
}
