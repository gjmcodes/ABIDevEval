using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories
{
    public interface IReadOnlyRepository<T> where T : Query
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllByIds(Guid[] ids);
        Task<IEnumerable<T>> GetAll();

    }
}
