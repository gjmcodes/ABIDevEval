using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public class UserReadOnlyRepository : ReadOnlyRepository<UserExternalQuery>, IUserReadOnlyRepository
    {
    }
}
