
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Query
    {
        public async Task<IEnumerable<T>> GetAllByIds(Guid[] ids)
        {
            return new List<T>();
        }

        public async Task<T> GetById(Guid id)
        {
            throw null;
        }
    }
}
