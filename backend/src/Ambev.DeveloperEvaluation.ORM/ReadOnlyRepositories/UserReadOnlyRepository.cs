using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public class UserReadOnlyRepository : ReadOnlyRepository<UserExternalQuery>, IUserReadOnlyRepository
    {
        public const string COLLECTION_NAME = "Users";

        public UserReadOnlyRepository(ReadOnlyContext ctx) 
            : base(ctx, COLLECTION_NAME)
        {
        }
    }
}
