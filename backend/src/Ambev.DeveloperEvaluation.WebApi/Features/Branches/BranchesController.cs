using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : BaseController
    {

        private readonly IBranchReadOnlyRepository _branchReadOnlyRepository;

        public BranchesController(IBranchReadOnlyRepository branchReadOnlyRepository)
        {
            _branchReadOnlyRepository = branchReadOnlyRepository;
        }

        /// <summary>
        /// Retrieves all branches
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The branches details if found</returns>
        [HttpGet]
        [ProducesResponseType(typeof(BranchExternalQuery[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var branches = await _branchReadOnlyRepository.GetAll();

            if (branches == null || branches.Count() == 0)
                return NotFound("No branches found");

            return Ok(branches.ToArray());
        }
    }
}
