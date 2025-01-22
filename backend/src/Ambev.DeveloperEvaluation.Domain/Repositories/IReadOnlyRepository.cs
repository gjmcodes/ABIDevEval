
using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IReadOnlyRepository<T> where T : Query
    {
        Task<T> GetById(Guid id);
    }
}
