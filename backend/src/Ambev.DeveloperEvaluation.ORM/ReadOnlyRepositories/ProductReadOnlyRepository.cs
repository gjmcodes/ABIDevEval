
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public class ProductReadOnlyRepository : ReadOnlyRepository<ProductExternalQuery>, IProductReadOnlyRepository
    {
        const string COLLECTION_NAME = "Products";

        public ProductReadOnlyRepository(ReadOnlyContext ctx) 
            : base(ctx, COLLECTION_NAME)
        {
        }
    }
}
