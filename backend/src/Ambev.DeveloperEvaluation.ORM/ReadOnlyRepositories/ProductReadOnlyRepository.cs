
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    internal class ProductReadOnlyRepository : ReadOnlyRepository<ProductExternalQuery>, IProductReadOnlyRepository
    {
    }
}
