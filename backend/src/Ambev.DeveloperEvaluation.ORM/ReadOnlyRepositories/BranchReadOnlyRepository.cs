using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public class BranchReadOnlyRepository : ReadOnlyRepository<BranchExternalQuery>, IBranchReadOnlyRepository
    {
        public const string COLLECTION_NAME = "Branches";
        public BranchReadOnlyRepository(ReadOnlyContext ctx)
            : base(ctx, COLLECTION_NAME)
        {
        }
    }
}
