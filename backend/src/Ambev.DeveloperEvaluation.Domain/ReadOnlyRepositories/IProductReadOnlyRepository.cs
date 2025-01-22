using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories
{
    public interface IProductReadOnlyRepository : IReadOnlyRepository<ProductExternalQuery>
    {
    }
}
